using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SpineWise.Web.Data;
using SpineWise.Web.Endpoints.SpinePostureDataLog.GetLastXDays;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Services.SignalR;

namespace SpineWise.Web.Endpoints.SpinePostureDataLog.SpecificDayData
{
    [MyAuthorization("user")]
    public class GetSpecificDayDataEndpoint : MyBaseEndpoint<GetSpecificDayDataRequest, GetSpecificDayDataResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;
        public GetSpecificDayDataEndpoint(ApplicationDbContext context, MyAuthService myAuth)
        {
            _applicationDbContext = context;
            _myAuthService = myAuth;
        }
        [HttpGet("data")]
        public override async Task<ActionResult<GetSpecificDayDataResponse>> Action([FromQuery]GetSpecificDayDataRequest request, CancellationToken cancellationToken = default)
        {
            var loggedUser = _myAuthService.GetAuthInfo().UserAccount;

            var user=_applicationDbContext
                .Users
                .Include(x=>x.Chair)
                .Where(x=>x.Id==loggedUser.Id)
                .FirstOrDefault();
            if (user==null)
            {
                return BadRequest("Wrong user ID");
            }

            var chair = user.Chair;

            var query = _applicationDbContext
                .SpinePostureDataLogs
                .Where(x => x.DateTime.Date == request.Datum.Date && x.ChairId==chair.Id);

            var ukupno = query.Count();

            var ukupnoLose = query.Where(x => !x.Good).Count();
            var ukupnoDobri = query.Where(x => x.Good).Count();

            var s1 = query.Where(x => x.PressureSensor1).Count();
            var s2 = query.Where(x => x.PressureSensor2).Count();
            var s3 = query.Where(x => x.PressureSensor3).Count();
            float p1, p2, p3;
            if (ukupno > 0)
            {
                p1 = (float)s1 / ukupno;
                p2 = (float)s2 / ukupno;
                p3 = (float)s3 / ukupno;
            }
            else
            {
                var returnObj = new GetSpecificDayDataResponse
                {
                    BadPercentage = 0,
                    GoodPercentage = 0,
                    s1Percentage = 0,
                    s2Percentage = 0,
                    s3Percentage = 0,
                    SittingHours = 0,
                    SittingMinutes=0,
                    Datum=request.Datum
                };
                return Ok(returnObj);
            }
            var lastDaysSum = new List<GetLastXSittingDataResponse>();
            var lastXDaysMinutes = await query.ToListAsync(cancellationToken);

            var newDate = new GetLastXSittingDataResponse()
            {
                Date = lastXDaysMinutes.FirstOrDefault().DateTime.Date
            };

            newDate.TotalMinutes = lastXDaysMinutes
                .Zip(lastXDaysMinutes.Skip(1), (firstLog, secondLog) => (secondLog.DateTime - firstLog.DateTime).TotalMinutes)
                .Where(diff => diff < 5)
                .Sum();

            int sittingHours = (int)newDate.TotalMinutes / 60;
            int sittingMinutes = ((int)newDate.TotalMinutes - sittingHours * 60);

            var obj = new GetSpecificDayDataResponse
            {
                SittingHours = sittingHours,
                SittingMinutes = sittingMinutes,
                BadPercentage = (float)ukupnoLose/ukupno*100,
                GoodPercentage = (float)ukupnoDobri / ukupno*100,
                s1Percentage = p1,
                s2Percentage = p2,
                s3Percentage = p3,
                Datum=request.Datum,
            };

            obj.s1Percentage = (float)Math.Round(obj.s1Percentage, 2)*100f;
            obj.s2Percentage = (float)Math.Round(obj.s2Percentage, 2)*100f;
            obj.s3Percentage = (float)Math.Round(obj.s3Percentage, 2)*100f;
            obj.BadPercentage = (float)Math.Round(obj.BadPercentage, 2);
            obj.GoodPercentage = (float)Math.Round(obj.GoodPercentage, 2);

            //await _hub.Clients.All.SendAsync("user sitting data", obj);

            return Ok(obj);
        }
    }
}
