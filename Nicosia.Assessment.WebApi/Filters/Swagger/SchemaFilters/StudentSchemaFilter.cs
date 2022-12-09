using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Nicosia.Assessment.Application.Handlers.Student.Commands.AddNewStudent;
using Nicosia.Assessment.Application.Handlers.Student.Commands.Authenticate;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nicosia.Assessment.WebApi.Filters.Swagger.SchemaFilters
{
    public class StudentSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(AuthenticateStudentCommand))
            {
                schema.Example = new OpenApiObject()
                {
                    ["Username"] = new OpenApiString("Phoebe.Buffay@mail.com"),
                    ["Password"] = new OpenApiString("P@s$w0rD"),
                };
            }

            if (context.Type == typeof(AddNewStudentCommand))
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
