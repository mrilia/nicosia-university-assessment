using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nicosia.Assessment.WebApi.Filters.Swagger.DocumentFilters
{
    public class OrderTagsDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument openApiDoc, DocumentFilterContext context)
        {
            Dictionary<KeyValuePair<string, OpenApiPathItem>, int> paths = new Dictionary<KeyValuePair<string, OpenApiPathItem>, int>();
            foreach (var path in openApiDoc.Paths)
            {
                SwaggerOperationOrderAttribute orderAttribute = context.ApiDescriptions.FirstOrDefault(x => x.RelativePath.Replace("/", string.Empty)
                        .Equals(path.Key.Replace("/", string.Empty), StringComparison.InvariantCultureIgnoreCase))?
                    .ActionDescriptor?.EndpointMetadata?.FirstOrDefault(x => x is SwaggerOperationOrderAttribute) as SwaggerOperationOrderAttribute;
                
                int order = 1000;
                if (orderAttribute != null)
                    order = orderAttribute.Order;
                    //throw new ArgumentNullException("there is no order for operation " + path.Key);

                paths.Add(path, order);
            }

            var orderedPaths = paths.OrderBy(x => x.Value).ToList();
            openApiDoc.Paths.Clear();
            orderedPaths.ForEach(x => openApiDoc.Paths.Add(x.Key.Key, x.Key.Value));
        }
    }
}
