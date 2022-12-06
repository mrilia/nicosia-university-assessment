using System;

namespace Nicosia.Assessment.WebApi.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    { }
}
