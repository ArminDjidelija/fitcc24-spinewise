using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Models;

namespace SpineWise.Web.Endpoints.SpineWiseDataLog.GetLastDay
{
    [Route("logsdata")]
    public class GetLastDayDataEndpoint:MyBaseEndpoint<GetLastDayDataRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public GetLastDayDataEndpoint(ApplicationDbContext context, MyAuthService myAuth)
        {
            _applicationDbContext = context;
            _myAuthService = myAuth;
        }
        [HttpGet("getlastdayminutes")]
        public override async Task<ActionResult<NoResponse>> Action([FromQuery]GetLastDayDataRequest request, CancellationToken cancellationToken = default)
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

            var lastLog = await _applicationDbContext.SpineWiseDataLogs
                .Where(log => log.ChairId == chairId)
                .MaxAsync(log => (DateTime?)log.LogDateTime);

            if (lastLog.HasValue)
            {
                var logsFromLastDay = await _applicationDbContext.SpineWiseDataLogs
                    .Where(log => log.ChairId == chairId && log.LogDateTime.Date == lastLog.Value.Date)
                    .ToListAsync();

                var timeDifferences = logsFromLastDay
                    .Zip(logsFromLastDay.Skip(1), (firstLog, secondLog) => new
                    {
                        FirstLog = firstLog,
                        SecondLog = secondLog,
                        TimeDifference = (secondLog.LogDateTime - firstLog.LogDateTime).TotalMinutes
                    })
                    .Where(pair => pair.TimeDifference < 30)
                    .ToList();

                var totalMinutes = timeDifferences.Sum(x => x.TimeDifference);

                return Ok(totalMinutes);
            }

            return Ok(0);
        }

        
    }
}
