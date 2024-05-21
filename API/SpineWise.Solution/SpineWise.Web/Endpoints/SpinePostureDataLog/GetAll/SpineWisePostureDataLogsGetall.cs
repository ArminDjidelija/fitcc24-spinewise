using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Models;

namespace SpineWise.Web.Endpoints.SpinePostureDataLog.GetAll
{
    [Route("sensorlogs")]
    public class SpineWisePostureDataLogsGetall:MyBaseEndpoint<NoRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SpineWisePostureDataLogsGetall(ApplicationDbContext context, MyAuthService myAuth)
        {
            _applicationDbContext = context;
        }

        [HttpGet("getall")]
        public override async Task<ActionResult<NoResponse>> Action([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
        {
            var data = _applicationDbContext
                .SpinePostureDataLogs
                .Count();
            return Ok("Broj posture datalogs u bazi je: "+data);
        }
    }
}
