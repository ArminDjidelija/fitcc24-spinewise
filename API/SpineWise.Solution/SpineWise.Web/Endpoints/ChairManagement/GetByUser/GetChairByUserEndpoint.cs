using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Models;

namespace SpineWise.Web.Endpoints.ChairManagement.GetByUser
{
    [MyAuthorization("user")]
    [Route("chair")]
    public class GetChairByUserEndpoint:MyBaseEndpoint<NoRequest, GetChairByUserResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public GetChairByUserEndpoint(ApplicationDbContext context, MyAuthService myAuthService)
        {
            _applicationDbContext = context;
            _myAuthService = myAuthService;
        }

        [HttpGet("getbyuser")]
        public override async Task<ActionResult<GetChairByUserResponse>> Action([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
        {
            var user = _myAuthService.GetAuthInfo().UserAccount;
            if (user == null)
            {
                return BadRequest("No user logged in!");
            }

            var userDb = await _applicationDbContext.Users.Include(x => x.Chair).Include(x=>x.Chair.ChairModel).Where(x => x.Id == user.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (userDb == null)
            {
                return BadRequest("Problem with user");
            }
            var chair = userDb.Chair;
            if (chair == null)
            {
                return Ok();
            }
            return Ok(new GetChairByUserResponse
            {
                Id=chair.Id,
                DateOfCreating = chair.DateOfCreating,
                SerialNumber = chair.SerialNumber,
                ChairModelName = chair.ChairModel.Name,
                Delay=chair.Delay,
                Naziv=chair.Naziv,
                SaljiPodatke = chair.SendData,
            });
        }
    }
}
