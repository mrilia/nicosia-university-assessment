using System;

namespace Nicosia.Assessment.WebApi.Filters.Swagger.DocumentFilters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SwaggerOperationOrderAttribute : Attribute
    {
        public int Order { get; }

        public SwaggerOperationOrderAttribute(int order)
        {
            this.Order = order;
        }
    }
}
