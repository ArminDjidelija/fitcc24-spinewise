using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpineWise.Web.Data;
using SpineWise.Web.Endpoints.SpineWiseDataLog.GetLast5Days;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;

namespace SpineWise.Web.Endpoints.SpinePostureDataLog.GetLastXDays
{
    //[MyAuthorization("user,superuser")]
    [Route("lastndays")]
    public class GetLastXSittingDataEndpoint:MyBaseEndpoint<int, GetLastXSittingDataResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public GetLastXSittingDataEndpoint(ApplicationDbContext context, MyAuthService myAuth)
        {
            _applicationDbContext = context;
            _myAuthService = myAuth;
        }

        [HttpGet("get")]
        public override async Task<ActionResult<GetLastXSittingDataResponse>> Action(int request, CancellationToken cancellationToken = default)
        {
            var userLogged = _myAuthService.GetAuthInfo().UserAccount;
            if (userLogged == null)
            {
                return BadRequest("User not signed");
            }

            var user = await _applicationDbContext.Users.
                Include(x => x.Chair).
                Where(x => x.Id == userLogged.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var chairId = user.ChairId;

            if (chairId == null)
            {
                return BadRequest("no chair assigned");
            }

            var lastXDaysMinutes = await _applicationDbContext.SpinePostureDataLogs
                .Where(log => log.ChairId == chairId)
                .GroupBy(log => log.DateTime.Date)
                .OrderBy(group => group.Key)
                .Take(request)
                .Select(group => new
                {
                    Date = group.Key,
                    Logs = group.OrderBy(log => log.DateTime).ToList()
                })
                .OrderByDescending(x => x.Date)
                .Take(request)
                .OrderBy(x=>x.Date)
                .ToListAsync(cancellationToken);

            var lastDaysSum = new List<GetLastXSittingDataResponse>();

            foreach (var day in lastXDaysMinutes)
            {
                var newDate = new GetLastXSittingDataResponse()
                {
                    Date = day.Date
                };

                newDate.TotalMinutes = day.Logs
                    .Zip(day.Logs.Skip(1), (firstLog, secondLog) => (secondLog.DateTime - firstLog.DateTime).TotalMinutes)
                    .Where(diff => diff < 5) //mora biti razlika intervala manja od 5 minuta da bi se registrovalo sjedenje, odnosno vrijeme sjedenja
                    .Sum();

                lastDaysSum.Add(newDate);
            }

            return Ok(lastDaysSum);
        }
    }
}
