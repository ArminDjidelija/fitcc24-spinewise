//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SpineWise.Web.Data;
//using SpineWise.Web.Helpers.Endpoint;
//using SpineWise.Web.Helpers.Models;

//namespace SpineWise.Web.Endpoints.SpinePostureDataLog
//{
//    [Route("bad")]
//    public class DeletaBad:MyBaseEndpoint<NoRequest,NoResponse>
//    {
//        private readonly ApplicationDbContext _applicationDbContext;

//        public DeletaBad(ApplicationDbContext context)
//        {
//            _applicationDbContext = context;
//        }

//        [HttpGet("modify")]
//        public override async Task<ActionResult<NoResponse>> Action([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
//        {
//            var data =await _applicationDbContext.SpinePostureDataLogs.ToListAsync(cancellationToken);

//            float lastleg1 = 0;
//            float lastleg2 = 0;
//            float lastback1 = 0;
//            float lastback2 = 0;
//            var count=data.Count;

//            for (int i = 2; i < count; i++)
//            {
//                lastleg1 = data[i - 1].LegDistance;
//                lastleg2 = data[i - 2].LegDistance;

//                lastback1 = data[i - 1].UpperBackDistance;
//                lastback2 = data[i - 2].UpperBackDistance;

//                if (data[i].UpperBackDistance > 30)
//                {
//                    data[i].UpperBackDistance = float.Round((lastback1 + lastback2) / 2, 2);
//                    float proba = data[i].UpperBackDistance;
//                }
//                if (data[i].LegDistance > 34)
//                {
//                    data[i].LegDistance = float.Round((lastleg1 + lastleg2) / 2, 2);
//                    float proba = data[i].LegDistance;
//                }
//            }

//            await _applicationDbContext.SaveChangesAsync(cancellationToken);

//            return Ok();
//        }
//    }
//}
