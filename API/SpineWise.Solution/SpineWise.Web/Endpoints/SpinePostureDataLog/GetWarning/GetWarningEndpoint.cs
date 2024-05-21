using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpineWise.Web.Data;
using SpineWise.Web.Endpoints.SpinePostureDataLog.GetGoodBadRatio;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;

namespace SpineWise.Web.Endpoints.SpinePostureDataLog.GetWarning
{
    [Route("warning")]
    public class GetWarningEndpoint:MyBaseEndpoint<GetWarningRequest, GetWarningResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public GetWarningEndpoint(ApplicationDbContext context, MyAuthService myAuth)
        {
            _applicationDbContext = context;
            _myAuthService = myAuth;
        }

        [HttpGet("get")]
        public override async Task<ActionResult<GetWarningResponse>> Action([FromQuery]GetWarningRequest request, CancellationToken cancellationToken = default)
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
            var DatumVrijeme = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));
            var last5minutes = await _applicationDbContext.SpinePostureDataLogs
                .Where(log => (log.ChairId == chairId) && (EF.Functions.DateDiffMinute(log.DateTime, DatumVrijeme)<=5))
                .ToListAsync(cancellationToken);

            if (last5minutes.Count >= 6)
            {
                var badCount = last5minutes.Where(log => !log.Good).Count();
                var goodCount = last5minutes.Where(log => log.Good).Count();
                var sum = (float)(badCount + goodCount);

                var goodbadr = 100 * (float)(goodCount) / sum;
                var badgoodr = 100 * (float)(badCount) / sum;

                var top5 = last5minutes.OrderByDescending(o => o.DateTime).Take(5);
                var badCount5 = top5.Where(log => !log.Good).Count();
                var goodCount5 = top5.Where(log => log.Good).Count();
                var sum5 = (float)(badCount5 + goodCount5);

                var goodbadr5 = 100 * (float)(goodCount5) / sum5;
                var badgoodr5 = 100 * (float)(badCount5) / sum5;

                var obj = new GetWarningResponse()
                {
                    BadCount = badCount,
                    GoodCount = goodCount,
                    GoodBadRatio = goodbadr,
                    BadGoodRatio = badgoodr,
                    BadCount5 = badCount5,
                    GoodCount5 = goodCount5,
                    GoodBadRatio5 = goodbadr5,
                    BadGoodRatio5 = badgoodr5
                };

                return Ok(obj);

            }

            return Ok(new GetWarningResponse()
            {
                GoodBadRatio = 0,
                BadGoodRatio = 0,
                BadGoodRatio5 = 0,
                BadCount5 = 0,
                GoodCount5 = 0,
                GoodBadRatio5 = 0,
                BadCount = 0,
                GoodCount = 0,
            });



        }
    }
}
