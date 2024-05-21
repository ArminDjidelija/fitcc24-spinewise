using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Models;

namespace SpineWise.Web.Endpoints.SpineWiseDataLog.GetLast5Days
{
    [Route("lastxdays")]
    public class GetLastXDaysDataEndpoint:MyBaseEndpoint<int, GetLastXDaysDataResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public GetLastXDaysDataEndpoint(ApplicationDbContext context, MyAuthService myAuth)
        {
            _applicationDbContext = context;
            _myAuthService = myAuth;
        }

        [HttpGet("get")]
        public override async Task<ActionResult<GetLastXDaysDataResponse>> Action([FromQuery]int request, CancellationToken cancellationToken = default)
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

            var lastXDaysMinutes = await _applicationDbContext.SpineWiseDataLogs
                .Where(log => log.ChairId == chairId)
                .GroupBy(log => log.LogDateTime.Date)
                .OrderBy(group => group.Key)
                .Take(request)
                .Select(group => new
                {
                    Date = group.Key,
                    Logs = group.OrderBy(log => log.LogDateTime).ToList()  
                })
                .ToListAsync(cancellationToken);

            var lastDaysSum = new List<GetLastXDaysDataResponse>();

            foreach (var day in lastXDaysMinutes)
            {
                var newDate = new GetLastXDaysDataResponse()
                {
                    Date = day.Date
                };

                newDate.TotalMinutes = day.Logs
                    .Zip(day.Logs.Skip(1), (firstLog, secondLog) => (secondLog.LogDateTime - firstLog.LogDateTime).TotalMinutes)
                    .Where(diff => diff < 30)
                    .Sum();

                lastDaysSum.Add(newDate);
            }

            return Ok(lastDaysSum);

        }
    }
}
