using Microsoft.AspNetCore.Mvc;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Models;

namespace SpineWise.Web.Endpoints.AuthEndpoints.AccountInfo.Get
{
    [MyAuthorization("everybody")]
    public class GetAccountInfoEndpoint:MyBaseEndpoint<NoRequest, GetAccountInfoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public GetAccountInfoEndpoint(ApplicationDbContext context, MyAuthService myAuthService)
        {
            _applicationDbContext = context;
            _myAuthService = myAuthService;
        }

        [HttpGet("korisnik")]
        public override async Task<ActionResult<GetAccountInfoResponse>> Action([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
        {
            var user = _myAuthService.GetAuthInfo().UserAccount;

            if(user == null)
            {
                return BadRequest("Wrong token");
            }
            var obj = new GetAccountInfoResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return Ok(obj);
        }
    }
}
