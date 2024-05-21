using Microsoft.AspNetCore.Mvc;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Models;

namespace SpineWise.Web.Endpoints.AuthEndpoints.GetRole
{
    [Route("role")]
    public class GetRole:MyBaseEndpoint<NoRequest, GetRoleResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public GetRole(ApplicationDbContext context, MyAuthService myAuthService)
        {
            _applicationDbContext = context;
            _myAuthService = myAuthService;
        }
        [HttpGet("get")]
        public override async Task<ActionResult<GetRoleResponse>> Action([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
        {
            var logged = _myAuthService.GetAuthInfo().UserAccount;
            if (logged == null)
            {
                return Ok();
            }

            var user = await _applicationDbContext.UserAccounts.FindAsync(logged.Id);

            if (user == null)
            {
                return Ok();
            }

            var isUser = await _applicationDbContext.Users.FindAsync(logged.Id);

            if (isUser != null)
            {
                return new GetRoleResponse() { Role = "user" };
            }
            var isAdmin = await _applicationDbContext.SuperAdmins.FindAsync(logged.Id);
            if (isAdmin != null)
            {
                return new GetRoleResponse() { Role = "superadmin" };
            }

            return Ok();
        }
    }
}
