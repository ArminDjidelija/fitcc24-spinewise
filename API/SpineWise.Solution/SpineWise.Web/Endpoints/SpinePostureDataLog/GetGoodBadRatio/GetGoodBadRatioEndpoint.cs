using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpineWise.Web.Data;
using SpineWise.Web.Endpoints.SpinePostureDataLog.GetLastXDays;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;

namespace SpineWise.Web.Endpoints.SpinePostureDataLog.GetGoodBadRatio
{
    [Route("goodbadratio")]
    public class GetGoodBadRatioEndpoint:MyBaseEndpoint<int, GetGoodBadRatioResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public GetGoodBadRatioEndpoint(ApplicationDbContext context, MyAuthService myAuth)
        {
            _applicationDbContext = context;
            _myAuthService = myAuth;
        }

        [HttpGet("get")]
        public override async Task<ActionResult<GetGoodBadRatioResponse>> Action([FromQuery]int request, CancellationToken cancellationToken = default)
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
                .ToListAsync(cancellationToken);

            var lastDaysSum = new List<GetGoodBadRatioResponse>();

            foreach (var day in lastXDaysMinutes)
            {
                var newDate = new GetGoodBadRatioResponse()
                {
                    Date = day.Date
                };

                newDate.CountGood = day.Logs.Count(log => log.Good);
                newDate.CountBad = day.Logs.Count(log => !log.Good);
                var sum = newDate.CountGood + newDate.CountBad;
                if (sum > 0)
                {
                    newDate.RatioGood = float.Round(100 * (float)newDate.CountGood / (float)sum, 2);
                    newDate.RatioBad = float.Round(100 * (float)newDate.CountBad / (float)sum, 2);
                }
                else
                {
                    newDate.RatioBad = 0;
                    newDate.RatioGood = 0;
                }
                lastDaysSum.Add(newDate);
            }

            return Ok(lastDaysSum);
        }
    }
}
