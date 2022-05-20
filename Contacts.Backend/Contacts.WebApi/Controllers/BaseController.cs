using System;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Contacts.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {

        private IMediator _mediator = null!;
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>() ?? null!;

        private readonly bool _testUser = false;
        private readonly Guid _testUserID = Guid.Parse("20480835-FAA6-4495-8A7C-29E7CE175888");

        internal Guid UserId
        {
            get
            {
                if (_testUser)
                    return _testUserID;

                var result = Guid.Empty;

                if (User != null && User.Identity != null 
                    && User.Identity.IsAuthenticated)
                {
                    var claim = User.FindFirst(ClaimTypes.NameIdentifier);
                    if (claim != null)
                        Guid.TryParse(claim.Value, out result);
                }
                
                return result;
            }
        }
    }
}
