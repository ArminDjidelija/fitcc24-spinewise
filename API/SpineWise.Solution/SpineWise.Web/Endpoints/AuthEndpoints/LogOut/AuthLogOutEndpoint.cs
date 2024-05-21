using Microsoft.AspNetCore.Mvc;
using SpineWise.ClassLibrary.Models;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Loggers;
using SpineWise.Web.Helpers.Models;

namespace SpineWise.Web.Endpoints.AuthEndpoints.LogOut
{
    [Route("auth")]
    public class AuthLogOutEndpoint:MyBaseEndpoint<NoRequest, AuthLogOutResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ISignOutLogger _signOutLogger;
        private readonly MyAuthService _myAuthService;

        public AuthLogOutEndpoint(ApplicationDbContext context, ISignOutLogger signOutLogger, MyAuthService myAuthService)
        {
            _applicationDbContext = context;
            _signOutLogger = signOutLogger;
            _myAuthService = myAuthService;
        }

        [HttpPost("logout")]
        public override async Task<ActionResult<AuthLogOutResponse>> Action([FromBody] NoRequest request, CancellationToken cancellationToken = default)
        {
            UserToken? authToken = _myAuthService.GetAuthInfo().AuthUserToken;

            if (authToken == null)
            {
                return BadRequest(new AuthLogOutResponse());
            }

            var user = authToken.UserAccount;

            _applicationDbContext.Remove(authToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            await _signOutLogger.LogSignOut(user);

            return Ok(new AuthLogOutResponse());
        }
    }
}
