using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Nicosia.Assessment.Application.Handlers.Admin.Commands.AddNewAdmin;
using Nicosia.Assessment.Application.Handlers.Admin.Commands.Authenticate;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nicosia.Assessment.WebApi.Filters.Swagger.SchemaFilters
{
    public class AdminSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(AuthenticateAdminCommand))
            {
                schema.Example = new OpenApiObject()
                {
                    ["Username"] = new OpenApiString("Gunther.central.perk@mail.com"),
                    ["Password"] = new OpenApiString("P@s$w0rD"),
                };
            }

            if (context.Type == typeof(AddNewAdminCommand))
            {
                schema.Example = new OpenApiObject()
                {
                    ["Firstname"] = new OpenApiString("John"),
                    ["Lastname"] = new OpenApiString("Doe"),
                    ["DateOfBirth"] = new OpenApiString("2000-01-01"),
                    ["PhoneNumber"] = new OpenApiString("+989120001234"),
                    ["Email"] = new OpenApiString("john.doe@uni.com"),
                    ["Password"] = new OpenApiString("P@s$w0rD"),
                };
            }
        }
    }
}
