using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Nicosia.Assessment.Application.AutoMapper;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Persistence.Context;
using Nicosia.Assessment.WebApi.Authorization.Services;
using Nicosia.Assessment.WebApi.Authorization;
using Nicosia.Assessment.WebApi.Middleware;
using Nicosia.Assessment.WebApi.Authorization.Helpers;

namespace Nicosia.Assessment.WebApi.Installer
{
    public class MvcInstaller : IInstaller

    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {

            // configure strongly typed settings object
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            // configure DI for application services
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUserService, UserService>();

            services
                .AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
                .AddJsonOptions(options => options.UseDateOnlyTimeOnlyStringConverters());

            services.AddMemoryCache();
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.SetIsOriginAllowed(origin => true)
                       .AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<IStudentContext>();

            services.AddControllers(opt => opt.Filters.Add<OnExceptionMiddleware>());


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new StudentMappingProfile());
                mc.AddProfile(new LecturerMappingProfile());
                mc.AddProfile(new CourseMappingProfile());
                mc.AddProfile(new PeriodMappingProfile());
                mc.AddProfile(new SectionMappingProfile(new SqliteDbContext(), new SqliteDbContext()));
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(options =>
            {
                options.UseDateOnlyTimeOnlyStringConverters();

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Nicosia Assessment Project",
                    Version = "v1.0",
                    Description = "Nicosia Assessment Project ASP.NET Web API",
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Please insert JWT with Bearer into field",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                } });

                options.CustomSchemaIds((Type x) => x.FullName);
            });
        }
    }
}
