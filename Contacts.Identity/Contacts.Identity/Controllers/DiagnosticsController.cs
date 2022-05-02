// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Contacts.Identity.ViewModels;
using Contacts.Identity.ViewModels.Diagnostics;

namespace Contacts.Identity.Controllers
{
    [SecurityHeaders]
    [Authorize]
    public class DiagnosticsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var connection = HttpContext.Connection;
            var remoteAddres = connection?.RemoteIpAddress;
            var localAddresses = new string[] { "127.0.0.1", "::1", connection?.LocalIpAddress?.ToString() ?? String.Empty};
            if (remoteAddres == null || !localAddresses.Contains(remoteAddres.ToString()))
            {
                return NotFound();
            }

            var model = new DiagnosticsViewModel(await HttpContext.AuthenticateAsync());
            return View(model);
        }
    }
}