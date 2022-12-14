using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Nicosia.Assessment.Application.AutoMapper;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Persistence.Context;
using Nicosia.Assessment.Shared.Token.JWT.Models;
using Nicosia.Assessment.WebApi.Filters.Swagger.DocumentFilters;
using Nicosia.Assessment.WebApi.Filters.Swagger.SchemaFilters;
using Nicosia.Assessment.WebApi.Middleware;

namespace Nicosia.Assessment.WebApi.Installer
{
    public class MvcInstaller : IInstaller

    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    // In addition, you can limit the depth
                    //options.JsonSerializerOptions.MaxDepth = 1;
                });

            // configure strongly typed settings object
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services
                .AddControllers(options => options.UseDateOnlyTimeOnlyStringConverters())
                .AddJsonOptions(options => options.UseDateOnlyTimeOnlyStringConverters());

            services.AddMemoryCache();
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<IStudentContext>();
            services.AddValidatorsFromAssemblyContaining<IAdminContext>();
            services.AddValidatorsFromAssemblyContaining<ILecturerContext>();
            services.AddValidatorsFromAssemblyContaining<ICourseContext>();
            services.AddValidatorsFromAssemblyContaining<IPeriodContext>();
            services.AddValidatorsFromAssemblyContaining<ISectionContext>();

            services.AddControllers(opt => opt.Filters.Add<OnExceptionMiddleware>());


            var useSqlite = bool.Parse(configuration["UseSqliteAsDatabaseEngine"]!);
            var usePostgres = bool.Parse(configuration["UsePostgresAsDatabaseEngine"]!);

            if (useSqlite)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AdminMappingProfile());
                    mc.AddProfile(new StudentMappingProfile());
                    mc.AddProfile(new LecturerMappingProfile());
                    mc.AddProfile(new CourseMappingProfile());
                    mc.AddProfile(new PeriodMappingProfile());
                    mc.AddProfile(new ApprovalRequestMappingProfile(new SqliteDbContext()));
                    mc.AddProfile(new SectionMappingProfile(new SqliteDbContext(), new SqliteDbContext()));
                });
                IMapper mapper = mappingConfig.CreateMapper();
                services.AddSingleton(mapper);
            }
            else if (usePostgres)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AdminMappingProfile());
                    mc.AddProfile(new StudentMappingProfile());
                    mc.AddProfile(new LecturerMappingProfile());
                    mc.AddProfile(new CourseMappingProfile());
                    mc.AddProfile(new PeriodMappingProfile());
                    mc.AddProfile(new ApprovalRequestMappingProfile(new PostgresDbContext()));
                    mc.AddProfile(new SectionMappingProfile(new PostgresDbContext(), new PostgresDbContext()));
                });
                IMapper mapper = mappingConfig.CreateMapper();
                services.AddSingleton(mapper);
            }


            services.AddSwaggerGen(options =>
            {
                options.UseDateOnlyTimeOnlyStringConverters();


                options.DocumentFilter<OrderTagsDocumentFilter>();

                options.SchemaFilter<AdminSchemaFilter>();
                options.SchemaFilter<LecturerSchemaFilter>();
                options.SchemaFilter<StudentSchemaFilter>();

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

                options.EnableAnnotations();
            });
        }
    }
}
