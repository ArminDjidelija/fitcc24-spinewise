using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpineWise.ClassLibrary.Models;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;

namespace SpineWise.Web.Endpoints.SpinePostureDataLog.WrongPosture
{
    public class WrongPostureEndpoint : MyBaseEndpoint<WrongPostureRequest, WrongPostureResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;
        public WrongPostureEndpoint(ApplicationDbContext context, MyAuthService myAuth)
        {
            _applicationDbContext = context;
            _myAuthService = myAuth;
        }

        [HttpGet("posture")]
        public override async Task<ActionResult<WrongPostureResponse>> Action([FromQuery]WrongPostureRequest request, CancellationToken cancellationToken = default)
        {
            var user = _myAuthService.GetAuthInfo().UserAccount;
            if (user == null)
                return BadRequest("Wrong user");

            var userChair = await _applicationDbContext
                .Users
                .Include(x => x.Chair).ThenInclude(x => x.ChairModel)
                .Where(x => x.Id == user.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var chair = userChair.Chair;

            var lastXDaysMinutes = await _applicationDbContext.SpinePostureDataLogs
               .Where(log => log.ChairId == chair.Id)
               .GroupBy(log => log.DateTime.Date)
               .OrderByDescending(group => group.Key)
               .Take(request.Days)
               .Select(group => new
               {
                   Date = group.Key,
                   Count = group.Count(),
                   Good=group.Count(x=>x.Good)
               })
               .OrderBy(x => x.Date)
               .OrderBy(x => x.Date)
               .ToListAsync(cancellationToken);

            var podaci = new List<WrongPostureDto>();

            foreach (var item in lastXDaysMinutes)
            {
                var good = (float)item.Good / (float)item.Count;
                var goodInt = (int)(good*100);
                podaci.Add(new WrongPostureDto { Datum = item.Date, Good = goodInt, DatumString=item.Date.ToString("dd.MM.yyyy.") });
            }

            return Ok(podaci   );
        }
        public class WrongPostureDto
        {
            public DateTime Datum { get; set; }
            public int Good { get; set; }
            public string DatumString { get; set; }
        }
    }
}
