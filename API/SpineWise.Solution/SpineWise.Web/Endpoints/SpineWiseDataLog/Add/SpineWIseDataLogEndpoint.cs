using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Models;

namespace SpineWise.Web.Endpoints.SpineWiseDataLog.Add
{
    [Route("spinedata")]
    public class SpineWIseDataLogEndpoint:MyBaseEndpoint<SpineWiseDataLogRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IConfiguration _configuration;
        public SpineWIseDataLogEndpoint(ApplicationDbContext context, IConfiguration configuration)
        {
            _applicationDbContext = context;
            _configuration=configuration;
        }

        [HttpPost("log")]
        public override async Task<ActionResult<NoResponse>> Action([FromBody]SpineWiseDataLogRequest request, CancellationToken cancellationToken = default)
        {
            string arduinoLogKey = _configuration.GetValue<string>("ArduinoLogKey:Key");

            if (request.Key != arduinoLogKey)
            {
                return BadRequest("Wrong key");
            }

            var log = new ClassLibrary.Models.SpineWiseDataLog()
            {
                ChairId = request.ChairId,
                LogDateTime = request.DateTimeOfLog,
                LegDistance = request.LegDistance,
                LumbarBackDistance = request.LumbarBackDistance,
                ThoracicBackDistance = request.ThoracicBackDistance,
            };
            _applicationDbContext.SpineWiseDataLogs.Add(log);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return Ok();

        }
    }
}
