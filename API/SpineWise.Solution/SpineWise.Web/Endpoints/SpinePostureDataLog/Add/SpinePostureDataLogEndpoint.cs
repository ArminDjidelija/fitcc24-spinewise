using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Models;
using SpineWise.Web.Services.SignalR;
using SpineWise_Web;

namespace SpineWise.Web.Endpoints.SpinePostureDataLog.Add
{
    [Route("sensordata")]
    public class SpinePostureDataLogEndpoint:MyBaseEndpoint<SpinePostureDataLogRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IConfiguration _configuration;
        private readonly IHubContext<SignalrHub> _hub;

        public SpinePostureDataLogEndpoint(ApplicationDbContext context, IConfiguration configuration, IHubContext<SignalrHub> hub)
        {
            _applicationDbContext = context;
            _configuration = configuration;
            _hub = hub;
        }
        [HttpPost("log")]
        public override async Task<ActionResult<NoResponse>> Action([FromBody]SpinePostureDataLogRequest request, CancellationToken cancellationToken = default)
        {
            string arduinoLogKey = _configuration.GetValue<string>("ArduinoLogKey:Key");
            if (request.Key != arduinoLogKey)
            {
                return BadRequest("Wrong key");
            }
            var DatumVrijeme = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));

            var tool = new AiPredictor();
            var good = await tool.GetPredictionAsync(request.UpperBackDistance, request.LegDistance,
                (request.PressureSensor1 ? 1.0f : 0.0f),
                (request.PressureSensor2 ? 1.0f : 0.0f),
                (request.PressureSensor3 ? 1.0f : 0.0f)
            );

            var obj = new ClassLibrary.Models.SpinePostureDataLog()
            {
                LegDistance = request.LegDistance,
                UpperBackDistance = request.UpperBackDistance,
                PressureSensor1 = request.PressureSensor1,
                PressureSensor2 = request.PressureSensor2,
                PressureSensor3 = request.PressureSensor3,
                ChairId = request.ChairId,
                DateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
                    TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time")),
                Good = good
            };

            _applicationDbContext.SpinePostureDataLogs.Add(obj);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            await _hub.Clients.All.SendAsync("user sitting data", "Poruka sa servera");

            return Ok();
        }
    }

    public class AiPredictor
    {
        public async Task<bool> GetPredictionAsync(float up, float leg, float p1, float p2, float p3)
        {
            // Load sample data
            var sampleData = new MLModelspine.ModelInput()
            {
                UpperBackDistance = up,
                LegDistance = leg,
                PressureSensor1 = p1,
                PressureSensor2 = p2,
                PressureSensor3 = p3,
            };

            // Load model and predict output
            var result = await Task.Run(() => MLModelspine.Predict(sampleData));
            var resultBool = false;
            var podatak = Convert.ToInt32(result.PredictedLabel);
            if (podatak == 1)
            {
                resultBool = true;
            }

            return resultBool;
        }
    }
}
