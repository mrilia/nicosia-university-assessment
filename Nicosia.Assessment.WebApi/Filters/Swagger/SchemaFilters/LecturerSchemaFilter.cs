using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Nicosia.Assessment.Application.Handlers.Lecturer.Commands.AddNewLecturer;
using Nicosia.Assessment.Application.Handlers.Lecturer.Commands.Authenticate;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nicosia.Assessment.WebApi.Filters.Swagger.SchemaFilters
{
    public class LecturerSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(AuthenticateLecturerCommand))
            {
                schema.Example = new OpenApiObject()
                {
                    ["Username"] = new OpenApiString("David.Scientist@mail.com"),
                    ["Password"] = new OpenApiString("P@s$w0rD"),
                };
            }

            if (context.Type == typeof(AddNewLecturerCommand))
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
