using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Authentication.Cookies
{
    public static class CookieAuthenticationExtentions
    {
        public static void DisableRedirectToPath(
            this CookieAuthenticationEvents events,
            Expression<Func<CookieAuthenticationEvents, Func<RedirectContext<CookieAuthenticationOptions>, Task>>> expr,
            string path, int statusCode)
        {
            string propertyName = ((MemberExpression)expr.Body).Member.Name;
            var oldHandler = expr.Compile().Invoke(events);

            Func<RedirectContext<CookieAuthenticationOptions>, Task> newHandler = context =>
            {
                if (context.Request.Path.StartsWithSegments(path))
                {
                    context.Response.StatusCode = statusCode;
                }
                else
                {
                    oldHandler(context);
                }

                return Task.CompletedTask;
            };

            typeof(CookieAuthenticationEvents).GetProperty(propertyName).SetValue(events, newHandler);
        }
    }
}
