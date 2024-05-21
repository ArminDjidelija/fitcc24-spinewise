using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Models;

namespace SpineWise.Web.Endpoints.UserChair.UserAddChair
{
    [Route("userchair")]
    public class UserChairAddEndpoint:MyBaseEndpoint<UserChairAddRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;
        public UserChairAddEndpoint(ApplicationDbContext context, MyAuthService myAuthService)
        {
            _applicationDbContext = context;
            _myAuthService = myAuthService;
        }

        [HttpPost("addchair")]
        public override async Task<ActionResult<NoResponse>> Action(UserChairAddRequest request, CancellationToken cancellationToken = default)
        {
            var chair = await _applicationDbContext.Chairs.Where(x => x.SerialNumber == request.SerialNumber)
                .FirstOrDefaultAsync(cancellationToken);

            if (chair == null)
            {
                return BadRequest();
            }

            var loggedUserAccount = _myAuthService.GetAuthInfo().UserAccount;
            if (loggedUserAccount == null)
            {
                return BadRequest();
            }
            var user = await _applicationDbContext.Users.FindAsync(loggedUserAccount.Id);

            if (user != null)
            {
                user.ChairId=chair.Id;
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
            }

            return Ok();
        }
    }
}
