using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;

namespace SpineWise.Web.Endpoints.SpinePostureDataLog.SpecificDayGraph
{

    public class SpecificDayGraphEndpoint : MyBaseEndpoint<SpecifiyDayGraphRequest, SpecifiyDayGraphResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;
        public SpecificDayGraphEndpoint(ApplicationDbContext context, MyAuthService myAuth)
        {
            _applicationDbContext = context;
            _myAuthService = myAuth;
        }
        [HttpGet("daygraph")]
        public override async Task<ActionResult<SpecifiyDayGraphResponse>> Action([FromQuery]SpecifiyDayGraphRequest request, CancellationToken cancellationToken = default)
        {
            var user = _myAuthService.GetAuthInfo().UserAccount;
            if (user == null)
                return BadRequest("Wrong user");

            var userChair = await _applicationDbContext
                .Users
                .Include(x=>x.Chair).ThenInclude(x=>x.ChairModel)
                .Where(x => x.Id == user.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var chair = userChair.Chair;

            var logs = _applicationDbContext
                .SpinePostureDataLogs
                .Where(x => x.ChairId == chair.Id && x.DateTime.Date == request.Datum.Date)
                .ToList();

            var delay = 60;//minutes

            DateTime minDateLog;
            DateTime maxDateLog;

            // Ako nema logova, postavi defaultne vrijednosti
            if (logs.Count == 0)
            {
                minDateLog = request.Datum.Date.AddHours(9);
                maxDateLog = request.Datum.Date.AddHours(16);
            }
            else
            {
                minDateLog = logs.Min(x => x.DateTime);
                maxDateLog = logs.Max(x => x.DateTime);
            }

            var startDate = minDateLog.Date.AddHours(minDateLog.Hour).AddMinutes(minDateLog.Minute - (minDateLog.Minute % delay));
            if (startDate > request.Datum.Date.AddHours(9))
            {
                startDate = request.Datum.Date.AddHours(9);
            }

            var endDate = maxDateLog.Date.AddHours(maxDateLog.Hour).AddMinutes(maxDateLog.Minute + (delay - (maxDateLog.Minute % delay)));
            if (endDate < request.Datum.Date.AddHours(16))
            {
                endDate = request.Datum.Date.AddHours(16);
            }

            if (request.Datum.Date == DateTime.Today)
            {
                var currentDateTime = DateTime.Now;
                maxDateLog = currentDateTime.Date.AddHours(currentDateTime.Hour).AddMinutes(currentDateTime.Minute + (delay - (currentDateTime.Minute % delay)));
                endDate = maxDateLog;
            }

            var podaci = new List<SittingDataDto>();

            for (var time = startDate; time < endDate; time = time.AddMinutes(delay))
            {
                var start = time;
                var end = start.AddMinutes(delay);

                var logsCopy = logs.Where(x => x.DateTime >= start && x.DateTime <= end);

                var minutes = logsCopy
                    .Zip(logsCopy.Skip(1), (firstLog, secondLog) => (secondLog.DateTime - firstLog.DateTime).TotalMinutes)
                    .Where(diff => diff < 3)
                    .Sum();
                minutes = Math.Round(minutes, 1);
                var newSittingData = new SittingDataDto
                {
                    IntervalStart = start.TimeOfDay,
                    IntervalEnd = end.TimeOfDay,
                    SittingMinutes = minutes,
                    Start = string.Format("{0:00}:{1:00}", start.TimeOfDay.Hours, start.TimeOfDay.Minutes),
                    End = string.Format("{0:00}:{1:00}", end.TimeOfDay.Hours, end.TimeOfDay.Minutes),
                    Interval = ""
                };
                newSittingData.Interval = newSittingData.Start + " - " + newSittingData.End;
                podaci.Add(newSittingData);
            }

            return Ok(podaci);
        }
        public class SittingDataDto
        {
            public TimeSpan IntervalStart { get; set; }
            public TimeSpan IntervalEnd { get; set; }
            public double SittingMinutes { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public string Interval { get; set; }
        }
    }
}
