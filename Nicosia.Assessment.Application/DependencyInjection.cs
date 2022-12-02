using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Nicosia.Assessment.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IMediator, Mediator>();


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = new
                    {
                        message =
                            actionContext.ModelState.Values.SelectMany(v => v.Errors)
                                .Select(e => e.ErrorMessage.ToString())
                                .FirstOrDefault()
                    };
                    return new BadRequestObjectResult(errors);
                };
            });

            return services;
        }
    }
}