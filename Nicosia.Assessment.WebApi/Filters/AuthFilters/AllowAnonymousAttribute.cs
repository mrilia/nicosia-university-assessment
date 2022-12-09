using System;

namespace Nicosia.Assessment.WebApi.Filters.AuthFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    { }
}
