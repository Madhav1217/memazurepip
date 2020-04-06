using Aliera.Utilities.Constants;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

namespace Aliera.Utilities
{
    public class UtilityHelper
    {
        /// <summary>
        /// Gets the remote IP asddress to be audited
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        public static string GetIP(IHttpContextAccessor httpContextAccessor)
        {
            var remoteIpAddress = string.Empty;
            if (httpContextAccessor.HttpContext.Request.Headers.ContainsKey("IPAddress"))
            {
                remoteIpAddress = httpContextAccessor.HttpContext.Request.Headers["IPAddress"];
            }

            if (String.IsNullOrEmpty(remoteIpAddress))
            {
                remoteIpAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }

            return remoteIpAddress;
        }

        /// <summary>
        /// Parse jwt and return SecurityToken
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        public static JwtSecurityToken ParseJwt(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var parsedToken = handler.ReadJwtToken(jwt);

            return parsedToken;
        }

        /// <summary>
        /// Get UserName from JWT
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        public static string GetUserName(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var parsedToken = handler.ReadJwtToken(jwt);

            return parsedToken.Subject;
        }

        /// <summary>
        /// Get UserId from JWT
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        public static long GetUserId(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var parsedToken = handler.ReadJwtToken(jwt);

            var userId = Convert.ToInt64(parsedToken.Claims.First(x => x.Type == BrokerConstants.Id).Value);

            return userId;
        }

        /// <summary>
        /// Get AppSettings Json file name by environment
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public static string GetAppSettingsJson(IHostingEnvironment env)
        {
            var appSettings = "appsettings.json";
            if (env.IsEnvironment(env.EnvironmentName))
            {
                appSettings = $"appsettings.{env.EnvironmentName}.json";
            }
            return appSettings;
        }

        /// <summary>
        /// Get AppSettings Json file name by environment
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public static string GetLoggerSettingsJson(IHostingEnvironment env)
        {
            var loggerSettings = "loggerSettings.json";
            if (env.IsEnvironment(env.EnvironmentName))
            {
                loggerSettings = $"loggerSettings.{env.EnvironmentName}.json";
            }
            return loggerSettings;
        }

        /// <summary>
        /// Get AWS Pipeline Parameter Value by Key
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static string GetParameterValueByKey(string parameterName)
        {
            // The parameter name is customized based on the ASPNETCORE_ENVIRONMENT           
            // You can change this to a fixed string or use a different mechanism to customize.
            // var parameterName = string.Format("/MyWebApp/{0}/constr", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

            // Using USEast1
            using (var ssmClient = new AmazonSimpleSystemsManagementClient(Amazon.RegionEndpoint.USEast1))
            {
                var response = ssmClient.GetParameterAsync(new GetParameterRequest
                {
                    Name = parameterName,
                    WithDecryption = true
                });
                return response.Result.Parameter.Value;
            }
        }

        public static T GetEnumByValue<T>(int enumValue)
        {
            return (T)(object)enumValue;
        }

        public static bool Validate(string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool Validate(long value)
        {
            return value > 0;
        }

        /// <summary>
        /// HttpClient PostAsJson async method
        /// </summary>
        /// <param name="uriString"></param>
        /// <param name="requestUri"></param>
        /// <param name="objectValue"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostAsync(string uriString, string requestUri, object objectValue)
        {
            using (var httpResponseMessage = new HttpResponseMessage())
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(uriString);
                    await client.PostAsJsonAsync(requestUri, objectValue);
                    return httpResponseMessage;
                }
            }
        }
        /// <summary>
        /// HttpClient PostAsJson async method with Access Token
        /// </summary>
        /// <param name="uriString"></param>
        /// <param name="requestUri"></param>
        /// <param name="objectValue"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostAsync(string uriString, string requestUri, object objectValue, string accessToken)
        {
            using (var httpResponseMessage = new HttpResponseMessage())
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(uriString);
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    await client.PostAsJsonAsync(requestUri, objectValue);
                    return httpResponseMessage;
                }
            }
        }
        public static string ReplaceParametersWithValues(Type type, object data, string text)
        {
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var parameter = "$" + property.Name.ToLower();

                if (text.ToLower().Contains(parameter))
                {
                    int replaceIndex = text.ToLower().IndexOf(parameter);
                    if (replaceIndex == -1)
                        break;
                    text = text
                        .Replace(text.Substring(replaceIndex, parameter.Length),
                        (type.GetProperty(property.Name)
                        .GetValue(data, null) != null) ? type.GetProperty(property.Name)
                        .GetValue(data, null).ToString() : string.Empty);
                }
            }
            return text;
        }

    }
}
