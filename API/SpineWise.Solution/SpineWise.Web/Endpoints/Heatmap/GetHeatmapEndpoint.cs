using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;

namespace SpineWise.Web.Endpoints.Heatmap
{
   // [MyAuthorization("user")]
    public class GetHeatmapEndpoint : MyBaseEndpoint<GetHeatmapRequest, GetHeatmapResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;

        public GetHeatmapEndpoint(ApplicationDbContext context, MyAuthService myAuthService)
        {
            _applicationDbContext = context;
            _myAuthService = myAuthService;
        }
        [HttpGet("heatmap")]
        public override async Task<ActionResult<GetHeatmapResponse>> Action([FromQuery]GetHeatmapRequest request, CancellationToken cancellationToken = default)
        {
            var user1 = _myAuthService.GetAuthInfo().UserAccount;

            var user = _applicationDbContext
                .Users
                .Include(x => x.Chair)
                .Where(x => x.Id == user1.Id)
                .FirstOrDefault();
                
            if(user== null)
            {
                return BadRequest();
            }

            var chair =user.Chair;

            var query = _applicationDbContext.SpinePostureDataLogs.Where(x => x.DateTime.Date == request.Datum.Date && x.ChairId==chair.Id);

            if(!query.Any()) {
                return BadRequest("Nema zapisa za taj dan!");
            }


            int ukupnoZapisa=query.Count();

            var s1 = query.Where(x => x.PressureSensor1).Count();
            var s2 = query.Where(x => x.PressureSensor2).Count();
            var s3 = query.Where(x => x.PressureSensor3).Count();
            var s4 = query.Where(x => x.PressureSensor4).Count();
            //var s4 = query.Where(x => x.PressureSensor3).Count();
            //var s5 = query.Where(x => x.PressureSensor3).Count();

            var response = new GetHeatmapResponse()
            {
                ChairId = chair.Id,
                Datum = request.Datum,
                s1Percentage = (float)s1 / ukupnoZapisa,
                s2Percentage = (float)s2 / ukupnoZapisa,
                s3Percentage = (float)s3 / ukupnoZapisa,
                s4Percentage = (float)s4 / ukupnoZapisa,
                BrojZapisa =ukupnoZapisa
            };

            response.s1Percentage = (float)Math.Round(response.s1Percentage, 2)*100f;
            response.s2Percentage = (float)Math.Round(response.s2Percentage, 2)*100f;
            response.s3Percentage = (float)Math.Round(response.s3Percentage, 2)*100f;
            response.s4Percentage = (float)Math.Round(response.s4Percentage, 2)*100f;

            return Ok(response);
        }
    }
}
