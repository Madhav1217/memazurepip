using Aliera.DatabaseEntities.Models;
using Aliera.DatabaseEntities.Models.Auth;
using Aliera.MemberDataAccess;
using Aliera.MemberService;
using Aliera.Utilities;
using Aliera.Utilities.AuditLog;
using Aliera.Utilities.Helpers;
using Aliera.Utilities.Logging.CustomExceptions;
using Aliera.Utilities.Logging.LoggerFactory;
using Aliera.Utilities.Logging.Middleware;
using Aliera.Utilities.Notifications;
using Aliera.Utilities.RHIntegration;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json.Serialization;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;

namespace Aliera.MemberWorkflow
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public LoggerSettings LoggerSettings { get; set; }
        public bool IsDevelopment { get; set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            string appSettingJson = UtilityHelper.GetAppSettingsJson(env);
            string loggerSettingJson = UtilityHelper.GetLoggerSettingsJson(env);

            LoggerSettings = new LoggerSettings();

            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(appSettingJson, false, true)
               .AddJsonFile(loggerSettingJson, false, true);

            builder.AddEnvironmentVariables();
            IsDevelopment = env.IsDevelopment();
            AppSettings.Environment = env.EnvironmentName;

            Configuration = builder.Build();
            Configuration.GetSection(nameof(LoggerSettings)).Bind(LoggerSettings);

            ICustomLoggerFactory cm = new CustomLoggerFactory(LoggerSettings);
            cm.Information($@"Environment= {env.EnvironmentName} ; Application = MemberWorkflow; appSettings selected= {UtilityHelper.GetAppSettingsJson(env)} ");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MemberPortalApi" });
            });

            var connectionString = string.Empty;
            var authConnectionString = string.Empty;
            if (IsDevelopment)
            {
                connectionString = Configuration.GetConnectionString("DefaultConnection");
                authConnectionString = Configuration.GetConnectionString("AuthConnection");
            }
            else
            {
              connectionString = Configuration.GetConnectionString("DefaultConnection");
                authConnectionString = Configuration.GetConnectionString("AuthConnection");
            }

            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<AuthContext>((serviceProvider, options) =>
                        options.UseSqlServer(authConnectionString
                        , serverDbContextOptionsBuilder =>
                        {
                            var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
                            serverDbContextOptionsBuilder.CommandTimeout(minutes);
                        })
                         .UseInternalServiceProvider(serviceProvider));
            services.AddDbContext<AuthContext>(ServiceLifetime.Scoped);

            services
                   .AddEntityFrameworkSqlServer()
                   .AddDbContext<Context>((serviceProvider, options) =>
                       options.UseSqlServer(connectionString,
                       serverDbContextOptionsBuilder =>
                       {
                           var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
                           serverDbContextOptionsBuilder.CommandTimeout(minutes);
                       })
                       .UseInternalServiceProvider(serviceProvider));
            services.AddDbContext<Context>(ServiceLifetime.Scoped);
            services.AddCors();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<AppSettings>(Configuration.GetSection("MailSettings"));
            services.Configure<AppSettings>(Configuration.GetSection("SmsSettings"));

            //override Parameter Store Keys with Values
            if (IsDevelopment)
            {
                services.Configure<AppSettings>(ops => ops.AccountSid = UtilityHelper.GetParameterValueByKey(ops.AccountSid));
                services.Configure<AppSettings>(ops => ops.AuthToken = UtilityHelper.GetParameterValueByKey(ops.AuthToken));
                services.Configure<AppSettings>(ops => ops.Admin123User = UtilityHelper.GetParameterValueByKey(ops.Admin123User));
                services.Configure<AppSettings>(ops => ops.Admin123Password = UtilityHelper.GetParameterValueByKey(ops.Admin123Password));
                services.Configure<AppSettings>(ops => ops.UserEmail = UtilityHelper.GetParameterValueByKey(ops.UserEmail));
                services.Configure<AppSettings>(ops => ops.Password = UtilityHelper.GetParameterValueByKey(ops.Password));
                services.Configure<AppSettings>(ops => ops.RHAPIKey = UtilityHelper.GetParameterValueByKey(ops.RHAPIKey));
                services.Configure<AppSettings>(ops => ops.RHUserName = UtilityHelper.GetParameterValueByKey(ops.RHUserName));
                services.Configure<AppSettings>(ops => ops.RHPassword = UtilityHelper.GetParameterValueByKey(ops.RHPassword));
                services.Configure<AppSettings>(ops => ops.RHKeyID = UtilityHelper.GetParameterValueByKey(ops.RHKeyID));
            }

            ConfigureDependencies(services);
            services.AddMvcCore()
            .AddAuthorization()
            .AddJsonFormatters()
            .AddJsonOptions(options =>
             {
                 options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
             });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication("Bearer")
          .AddJwtBearer("Bearer", options =>
          {
              options.Authority = Configuration.GetValue<string>("AppSettings:Authority");//"http://54.174.140.29:6300";//"http://localhost:6200";
              options.RequireHttpsMetadata = false;
              options.Audience = "memberapi";
          });

            services.AddMemoryCache();
            services.AddSingleton(LoggerSettings);
            Logger.InjectSerilLog(services, Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);           

            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            //Set audit constants
            AuditLogHelper.AuditSetTimeout = Configuration.GetValue<int>("AppSettings:AuditSetTimeout");
            AuditLogHelper.AuditQueueLimit = Configuration.GetValue<int>("AppSettings:AuditQueueLimit");
            AuditLogHelper.AuditApiUrl = Configuration["AppSettings:AuditUri"];
            AuditLogHelper.AuditRequestApi = Configuration["AppSettings:AuditRequestApi"];
            AuditLogHelper.Timer.Interval = AuditLogHelper.AuditSetTimeout * 60000;
            AuditLogHelper.Timer.Start();
            AuditLogHelper.Timer.Elapsed += AuditLogHelper.OnTimedEvent;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MemberService API");
            });
            app.UserSerilogMiddleware();
            loggerFactory.AddSerilog();
            app.UseAuthentication();
            app.UseMvc();
        }

        public static void ConfigureDependencies(IServiceCollection services)
        {
            services.AddDbContext<AuthContext>().AddUnitOfWork<AuthContext>();
            services.AddDbContext<Context>().AddUnitOfWork<Context>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Service and data access dependencies
            services.AddScoped<IMemberRegistrationDataAccess, MemberRegistrationDataAccess>();
            services.AddScoped<IMemberRegistrationService, MemberRegistrationService>();
            services.AddScoped<IMemberVerifyDataAccess, MemberVerifyDataAccess>();
            services.AddScoped<IMemberVerifyService, MemberVerifyService>();
            services.AddScoped<IMasterDataAccess, MasterDataAccess>();
            services.AddScoped<IMasterService, MasterService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISMSService, SMSService>();
            services.AddScoped<IResetPasswordService, ResetPasswordService>();
            services.AddScoped<IResetPasswordDataAccess, ResetPasswordDataAccess>();
            services.AddScoped<IClaimsService, ClaimsService>();
            services.AddScoped<IClaimsDataAccess, ClaimsDataAccess>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IDashboardDataAccess, DashboardDataAccess>();
            services.AddScoped<IMemberDocumentAndFormService, MemberDocumentAndFormService>();
            services.AddScoped<IMemberDocumentAndFormDataAccess, MemberDocumentAndFormDataAccess>();
            services.AddScoped<IMemberFaqService, MemberFaqService>();
            services.AddScoped<IMemberFaqDataAccess, MemberFaqDataAccess>();
            services.AddScoped<IDemographicsService, DemographicsService>();
            services.AddScoped<IDemographicsDataAccess, DemographicsDataAccess>();
            services.AddScoped<IMemberFeedbackDataAccess, MemberFeedbackDataAccess>();
            services.AddScoped<IMemberFeedbackService, MemberFeedbackService>();
            services.AddScoped<IPaymentDataAccess, PaymentDataAccess>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IProviderSearchDataAccess, ProviderSearchDataAccess>();
            services.AddScoped<IProviderSearchService, ProviderSearchService>();
            services.AddScoped<IPlanDetailsDataAccess, PlanDetailsDataAccess>();
            services.AddScoped<IPlanDetailsService, PlanDetailsService>();
            services.AddScoped<IMemberMessageDataAccess, MemberMessageDataAccess>();
            services.AddScoped<IMemberMessageService, MemberMessageService>();
            services.AddScoped<IAccumulatorDataAccess, AccumulatorDataAccess>();
            services.AddScoped<IAccumulatorService, AccumulatorService>();
            services.AddScoped<IIDMemberDataAccess, IDMemberDataAccess>();
            services.AddScoped<IIDMemberService, IDMemberService>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddScoped<IDocConverter, Converter>();
            services.AddScoped<IRectangleGatewayService, RectangleGatewayService>();
            services.AddScoped<IMemberSearchDataAccess, MemberSearchDataAccess>();
            services.AddScoped<IMemberSearchService, MemberSearchService>();
        }
    }
}
