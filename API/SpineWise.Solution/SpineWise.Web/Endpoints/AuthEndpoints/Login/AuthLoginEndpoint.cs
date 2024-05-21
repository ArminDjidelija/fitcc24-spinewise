using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpineWise.ClassLibrary.Models;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Auth.PasswordHasher;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Generators;
using SpineWise.Web.Helpers.Loggers;
using SpineWise.Web.Helpers.Models;

namespace SpineWise.Web.Endpoints.AuthEndpoints.Login
{
    [Route("auth")]
    public class AuthLoginEndpoint:MyBaseEndpoint<AuthLoginRequest, AuthLoginResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ISignInLogger _signInLogger;

        public AuthLoginEndpoint(ApplicationDbContext context, ISignInLogger signInLogger)
        {
            _applicationDbContext=context;
            _signInLogger=signInLogger;
        }

        [HttpPost("login")]
        public override async Task<ActionResult<AuthLoginResponse>> Action([FromBody]AuthLoginRequest request, CancellationToken cancellationToken = default)
        {
            UserAccount? loggedUserAccount = await _applicationDbContext
                .UserAccounts
                .FirstOrDefaultAsync(x => x.Email == request.email);

            if (loggedUserAccount == null)
            {
                //wrong email
                return Unauthorized(new MyAuthInfo(null));
            }

            var isHashOk= await PasswordHasher.Verify(loggedUserAccount.Password, request.password);

            if (!isHashOk)
            {
                _signInLogger.LogSignIn(loggedUserAccount, false);
                return Unauthorized(new MyAuthInfo(null));
            }

            string signInToken=TokenGenerator.GenerateToken(32);

            var authToken = new UserToken()
            {
                TokenValue = signInToken,
                UserAccountId = loggedUserAccount.Id,
                TimeOfRecording = DateTime.Now
            };
            string role = "";

            var userCheck = await _applicationDbContext.Users.FindAsync(loggedUserAccount.Id);
            if (userCheck == null)
            {
                role = "admin";
            }
            else
            {
                role = "user";
            }

            var newToken = new AuthLoginResponse() { AuthTokenValue = signInToken, Role = role};
            _applicationDbContext.UserTokens.Add(authToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            await _signInLogger.LogSignIn(loggedUserAccount, true);

            return Ok(newToken);
        }
    }
}
