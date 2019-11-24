using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MOP.Resolver;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Security;
using System.Linq;
using System.Text;

namespace MOP.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IConfigurationSection appSettingsSection = Configuration.GetSection("AppSettings");

            AppSettings appSettings = appSettingsSection.Get<AppSettings>();
            ServiceResolver.ResolveServices(services, appSettings.SqlConnectionString);

            services.Configure<AppSettings>(appSettingsSection);
            byte[] key = Encoding.ASCII.GetBytes(appSettings.JwtSecret);

            services.AddCors(options => options.AddPolicy("AllowAll", p => p
              .SetIsOriginAllowed(x => _ = true)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()));
            services.AddOpenApiDocument(document =>
            {
                document.OperationProcessors.Add(new ApiVersionProcessor() { IncludedVersions = new string[] { "1.0" } });
                document.DocumentName = "v1";
                document.PostProcess = d => d.Info.Title = "Meetings Organizer Pro Web API v1.0 OpenAPI";
                document.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                document.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("JWT"));

            });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");
            app.UseRouting();

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUi3(config =>
                {
                    config.SwaggerRoutes.Add(new SwaggerUi3Route("v1", "/swagger/v1.json"));

                    config.Path = "/swagger";
                    config.DocumentPath = "/swagger/v1.json";
                });
            }

            app.UseOpenApi(config =>
            {
                config.DocumentName = "v1";
                config.Path = "/swagger/v1.json";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
