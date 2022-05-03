// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Mvc;
using Contacts.Identity.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Contacts.Identity.ViewModels;

namespace Contacts.Identity.Controllers
{
    public class SecurityHeadersAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result;
            if (result is not ViewResult)
                return;

            // also consider adding upgrade-insecure-requests once you have HTTPS in place for production
            //csp += "upgrade-insecure-requests;";
            // also an example if you need client images to be displayed from twitter
            // csp += "img-src 'self' https://pbs.twimg.com;";

            //var csp = "default-src 'self'; object-src 'none'; frame-ancestors 'none'; sandbox allow-forms allow-same-origin allow-scripts; base-uri 'self';";
            
            var values = new Dictionary<string, string>()
            {
                {"X-Content-Type-Options", "nosniff" }, // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Content-Type-Options
                {"X-Frame-Options", "SAMEORIGIN" },     // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options
                //{"Content-Security-Policy",  csp},      // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy
                //{"X-Content-Security-Policy", csp},     // and once again for IE
                {"Referrer-Policy", "no-referrer"}      // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Referrer-Policy
            };

            var headers = context.HttpContext.Response.Headers;

            foreach (var item in values)
                if (!headers.ContainsKey(item.Key))
                    headers.Add(item.Key, item.Value);

        }
    }
}
