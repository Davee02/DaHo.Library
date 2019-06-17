using System;
using Microsoft.AspNetCore.Mvc.Filters;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace DaHo.Library.AspNetCore.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class, AllowMultiple = true)]
    public class HeaderAttribute : ActionFilterAttribute
    {
        public string Name { get; set; }

        public string Value { get; set; }


        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if(!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Value))
                context.HttpContext.Response.Headers.Add(Name, Value);
        }
    }
}
