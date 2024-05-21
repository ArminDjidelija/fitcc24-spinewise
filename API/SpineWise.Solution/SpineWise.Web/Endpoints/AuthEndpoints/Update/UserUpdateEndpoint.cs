using Microsoft.AspNetCore.Mvc;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Auth.PasswordHasher;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Loggers;

namespace SpineWise.Web.Endpoints.AuthEndpoints.Update
{
    [MyAuthorization("everybody")]
    public class UserUpdateEndpoint : MyBaseEndpoint<UserUpdateRequest, UserUpdateResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public UserUpdateEndpoint(ApplicationDbContext context, MyAuthService myAuthService)
        {
            _applicationDbContext = context;
            _myAuthService = myAuthService;
        }
        [HttpPut("korisnik")]
        public override async Task<ActionResult<UserUpdateResponse>> Action(UserUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var user = _myAuthService.GetAuthInfo().UserAccount;

            if(request.Firstname=="" ||request.Lastname=="")
            {
                return BadRequest("Prazan string!");
            }

            user.FirstName=request.Firstname;
            user.LastName=request.Lastname;

            if(request.Password=="" && request.NewPassword=="" && request.NewPasswordConfirm == "")
            {
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return Ok();
            }

            if(request.NewPasswordConfirm!=request.NewPassword)
            {
                return BadRequest("Lozinke se ne podudaraju!");
            }

            var isHashOk = await PasswordHasher.Verify(user.Password, request.Password);

            if(!isHashOk)
            {
                return BadRequest("Pogrešna stara lozinka");
            }

            user.Password = await PasswordHasher.Hash(request.NewPassword);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
