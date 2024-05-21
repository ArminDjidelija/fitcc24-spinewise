using Microsoft.AspNetCore.Mvc;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Models;

namespace SpineWise.Web.Endpoints.AuthEndpoints.Authentication
{
    [MyAuthorization("user")]
    [Route("auth")]
    public class IsUserCheckEndpoint:MyBaseEndpoint<NoRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;
        public IsUserCheckEndpoint(ApplicationDbContext context, MyAuthService myAuthService)
        {
            _applicationDbContext = context;
            _myAuthService = myAuthService;
        }
        [HttpGet("isUser")]
        public override async Task<ActionResult<NoResponse>> Action([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
        {
            var isAdmin = await _myAuthService.IsUser();
            if (isAdmin)
            {
                return Ok();
            }
            return Unauthorized();
        }
    }
}
