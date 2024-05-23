using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Services.SignalR;

namespace SpineWise.Web.Endpoints.SpinePostureDataLog.ArduinoInfo
{
    public class ArduinoInfoEndpoint : MyBaseEndpoint<ArduinoInfoRequest, ArduinoInfoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IConfiguration _configuration;

        public ArduinoInfoEndpoint(ApplicationDbContext context, IConfiguration configuration)
        {
            _applicationDbContext = context;
            _configuration = configuration;
        }
        [HttpGet("arduinoinfo")]
        public override async Task<ActionResult<ArduinoInfoResponse>> Action([FromQuery]ArduinoInfoRequest request, CancellationToken cancellationToken = default)
        {
            var chair = await _applicationDbContext
                .Chairs
                .Where(x => x.Id == request.ChairId)
                .FirstAsync(cancellationToken);

            if(chair == null)
                return BadRequest("Pogrešan chair id!");

            var obj = new ArduinoInfoResponse()
            {
                Delay = chair.Delay,
                SendData = chair.SendData
            };

            return Ok(obj);
        }
    }
}
