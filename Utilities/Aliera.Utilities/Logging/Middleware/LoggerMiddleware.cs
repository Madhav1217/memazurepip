using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Logging.CustomExceptions;
using Aliera.Utilities.Logging.LoggerFactory;
using Enum = Aliera.Utilities.Logging.CustomExceptions.CustomErrorCodes;

namespace Aliera.Utilities.Logging.Middleware
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly LoggerSettings _settings;

        private readonly ICustomLoggerFactory _customLoggerFactory;

        public LoggerMiddleware(LoggerSettings settings, RequestDelegate next,
            ICustomLoggerFactory customLoggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _settings = settings;
            _customLoggerFactory = customLoggerFactory;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var stopwatch = Stopwatch.StartNew();
            var replace = string.Empty;
            var logger = Log.ForContext<LoggerMiddleware>();
            var injectedRequestStream = new MemoryStream();
            try
            {
                if (_settings.Default.Equals(BrokerConstants.DEBUG, StringComparison.OrdinalIgnoreCase))
                {
                    await LogRequestInfo(httpContext, stopwatch, logger, injectedRequestStream);
                }
                else
                {
                    await _next(httpContext);
                }
            }
            catch (FatalException ex)
            {
                LogFatalException(httpContext, ex, _settings);
            }
            catch (VerboseException ex)
            {
                LogVerboseException(httpContext, ex, _settings);
            }
            catch (CustomException ex)
            {
                LogCustomException(httpContext, stopwatch, ex, _settings);
            }
            catch (SystemException ex)
            {
                LogException(httpContext, stopwatch, ex, _settings);
            }
            catch (Exception ex)
            {
                LogException(httpContext, stopwatch, ex, _settings);
            }
            finally
            {
                injectedRequestStream.Dispose();
            }
        }

        private async Task LogRequestInfo(HttpContext httpContext, Stopwatch stopwatch, ILogger logger, MemoryStream memoryStream)
        {
            FormatRequest(httpContext, stopwatch, logger, _settings, memoryStream);
            var originalBodyStream = httpContext.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                httpContext.Response.Body = responseBody;
                await _next(httpContext);
                stopwatch.Stop();
                await FormatResponse(httpContext, stopwatch, logger);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private void LogException(HttpContext httpContext, Stopwatch sw, Exception ex, LoggerSettings settings)
        {
            sw.Stop();

            LogForErrorContext(httpContext)
                .Error(ex, settings.MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, 500, sw.Elapsed.TotalMilliseconds);

            if (settings.IsSqlServerLog)
            {
                MSSqlDbLog.Invoke(settings, httpContext, ex, settings.MessageTemplate, ex.Message, sw.Elapsed.TotalMilliseconds, (int)LogEventLevel.Error);
            }

            HandleException(httpContext, ex);

        }

        static void LogFatalException(HttpContext httpContext, Exception ex, LoggerSettings settings)
        {
            LogForErrorContext(httpContext)
                .Error(ex, settings.VerboseMessageTemplate, ex.Message, (int)CustomErrorCodes.Code.FatalErrorCode);


            if (settings.IsSqlServerLog)
            {
                MSSqlDbLog.Invoke(settings, httpContext, ex, settings.VerboseMessageTemplate, ex.Message, 0, (int)LogEventLevel.Fatal);
            }

            ResponseWriteAsync(httpContext, ex, (int)CustomErrorCodes.Code.FatalErrorCode);
        }

        static void LogVerboseException(HttpContext httpContext, Exception ex, LoggerSettings settings)
        {
            LogForErrorContext(httpContext)
                .Error(ex, settings.VerboseMessageTemplate, ex.Message, (int)CustomErrorCodes.Code.VerboseErrorCode);

            if (settings.IsSqlServerLog)
            {
                MSSqlDbLog.Invoke(settings, httpContext, ex, settings.VerboseMessageTemplate, ex.Message, 0, (int)LogEventLevel.Verbose);
            }

            ResponseWriteAsync(httpContext, ex, (int)CustomErrorCodes.Code.VerboseErrorCode);

        }

        static ILogger LogForErrorContext(HttpContext httpContext)
        {
            var request = httpContext.Request;

            var result = Log
                .ForContext(BrokerConstants.REQUEST_HEADERS, request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
                .ForContext(BrokerConstants.REQUEST_HOST, request.Host)
                .ForContext(BrokerConstants.REQUEST_PROTOCOL, request.Protocol);

            if (request.HasFormContentType)
            {
                result = result.ForContext(BrokerConstants.REQUEST_FORM, request.Form.ToDictionary(v => v.Key, v => v.Value.ToString()));
            }

            return result;
        }

        private void FormatRequest(HttpContext httpContext, Stopwatch sw,
            ILogger logger, LoggerSettings appSettings, MemoryStream injectedRequestStream)
        {
            string bodyAsText;
            using (var bodyReader = new StreamReader(httpContext.Request.Body))
            {
                bodyAsText = bodyReader.ReadToEnd();

                var bytesToWrite = System.Text.Encoding.UTF8.GetBytes(bodyAsText);
                injectedRequestStream.Write(bytesToWrite, 0, bytesToWrite.Length);
                injectedRequestStream.Seek(0, SeekOrigin.Begin);
                httpContext.Request.Body = injectedRequestStream;
            }

            if (!string.IsNullOrEmpty(bodyAsText))
            {
                _customLoggerFactory.Debug(_settings.MessageTemplateForPostRequest,
                          httpContext.Request.Path,
                          httpContext.Request.Method,
                          bodyAsText,
                          sw.Elapsed.TotalMilliseconds);

                if (_settings.IsSqlServerLog)
                {
                    MSSqlDbLog.Invoke(_settings,
                        httpContext,
                        null,
                        _settings.MessageTemplateForPostRequest,
                        bodyAsText,
                        sw.Elapsed.TotalMilliseconds,
                       (int)Enum.LogLevel.DebugModeRequest);
                }

            }
            else
            {
                _customLoggerFactory.Debug(_settings.MessageTemplateForGetRequest,
                     httpContext.Request.Path,
                     httpContext.Request.Method,
                     sw.Elapsed.TotalMilliseconds);

                if (_settings.IsSqlServerLog)
                {
                    MSSqlDbLog.Invoke(appSettings,
                        httpContext,
                        null,
                        _settings.MessageTemplateForGetRequest,
                        httpContext.Request.QueryString.Value,
                        sw.Elapsed.TotalMilliseconds,
                        (int)Enum.LogLevel.DebugModeRequest);
                }
            }
        }

        private async Task FormatResponse(HttpContext context, Stopwatch sw, ILogger logger)
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            context.Request.EnableBuffering();

            var response = await new StreamReader(context.Response.Body).ReadToEndAsync();

            context.Response.Body.Seek(0, SeekOrigin.Begin);

            _customLoggerFactory.Write(LogEventLevel.Debug, _settings.MessageTemplateForResponse, // GetCall  
                     context.Request.Path,
                     context.Response.StatusCode,
                     response,
                     sw.Elapsed.TotalMilliseconds);

            if (_settings.IsSqlServerLog)
            {
                MSSqlDbLog.Invoke(_settings,
                       context,
                       null,
                       _settings.MessageTemplateForResponse,
                       response,
                       sw.Elapsed.TotalMilliseconds,
                        (int)Enum.LogLevel.DebugModeResponse);
            }
        }

        private void HandleException(HttpContext context, Exception exception)
        {

            ExceptionDetails(context, exception, out HttpResponse response, out int statusCode, out string message, out string description, out string innerException);

            response.ContentType = BrokerConstants.CONTENT_TYPE;
            response.StatusCode = statusCode;
            response.WriteAsync(JsonConvert.SerializeObject(new BaseResponse
            {
                ErrorCode = statusCode
            }));
        }

        private static void ResponseWriteAsync(HttpContext context, Exception exception, int errorCode)
        {

            ExceptionDetails(context, exception, out HttpResponse response, out int statusCode, out string message, out string description, out string innerException);

            response.ContentType = BrokerConstants.CONTENT_TYPE;
            response.StatusCode = statusCode;
            response.WriteAsync(JsonConvert.SerializeObject(new FatalResponse
            {
                ErrorCode = errorCode
            }));
        }

        private static void ExceptionDetails(HttpContext context, Exception exception, out HttpResponse response, out int statusCode, out string message, out string description, out string innerException)
        {
            response = context.Response;
            statusCode = (int)HttpStatusCode.InternalServerError;
            message = BrokerConstants.UNEXPECTED_ERROR;
            description = BrokerConstants.UNEXPECTED_ERROR;
            innerException = BrokerConstants.UNEXPECTED_ERROR;
            if (exception != null)
            {
                message = exception.Message;
                description = exception.StackTrace;
                innerException = (exception.InnerException != null) ? exception.InnerException.ToString() : string.Empty;
            }
        }
        private void LogCustomException(HttpContext httpContext, Stopwatch sw, Exception ex, LoggerSettings settings)
        {
            sw.Stop();

            LogForErrorContext(httpContext)
                .Error(ex, settings.MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, 500, sw.Elapsed.TotalMilliseconds);

            if (settings.IsSqlServerLog)
            {
                MSSqlDbLog.Invoke(settings, httpContext, ex, settings.MessageTemplate, ex.Message, sw.Elapsed.TotalMilliseconds, (int)LogEventLevel.Error);
            }

            HandleCustomException(httpContext, ex);

        }
        private void HandleCustomException(HttpContext context, Exception exception)
        {
            CustomExceptionDetails(context, exception, out HttpResponse response, out int errorCode, out double statusCode);
            response.ContentType = BrokerConstants.CONTENT_TYPE;
            response.StatusCode = errorCode;
            response.WriteAsync(JsonConvert.SerializeObject(new CustomResponse
            {
                ErrorCode = statusCode,
                Message = (exception != null) ? exception.Message : string.Empty
            }));
        }
        private static void CustomExceptionDetails(HttpContext context, Exception exception, out HttpResponse response, out int errorCode, out double statusCode)
        {
            response = context.Response;
            errorCode = (int)HttpStatusCode.InternalServerError;
            statusCode = new CustomException().CustomExceptionCode(exception.Message);
        }
    }
}

