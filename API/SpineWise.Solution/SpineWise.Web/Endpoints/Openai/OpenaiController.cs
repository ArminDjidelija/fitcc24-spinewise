using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenAI_API;
using SpineWise.Web.Data;
using SpineWise.Web.Endpoints.SpinePostureDataLog.GetGoodBadRatio;
using SpineWise.Web.Endpoints.SpinePostureDataLog.GetLastXDays;
using SpineWise.Web.Endpoints.SpinePostureDataLog.GetWarning;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Models;
using System.Linq;

namespace SpineWise.Web.Endpoints.Openai
{
    [MyAuthorization("user")]
    [Route("aicontroller")]
    public class OpenaiController:MyBaseEndpoint<NoRequest, string>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly MyAuthService _myAuthService;
        private readonly IConfiguration _configuration;

        public OpenaiController(ApplicationDbContext context, MyAuthService myAuthService, IConfiguration configuration)
        {
            _applicationDbContext = context;
            _myAuthService = myAuthService;
            _configuration = configuration;
        }
        [HttpGet("getresponse")]
        public override async Task<ActionResult<string>> Action([FromQuery] NoRequest request, CancellationToken cancellationToken = default)
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
            var DatumVrijeme = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));
            var last5minutes = await _applicationDbContext.SpinePostureDataLogs
                .Where(log => (log.ChairId == chairId) && (EF.Functions.DateDiffMinute(log.DateTime, DatumVrijeme) <= 5))
                .ToListAsync(cancellationToken);

            var lastXDaysMinutes = await _applicationDbContext.SpinePostureDataLogs
                .Where(log => log.ChairId == chairId)
                .GroupBy(log => log.DateTime.Date)
                .OrderByDescending(group => group.Key)
                .Take(1)
                .Select(group => new
                {
                    Date = group.Key,
                    Logs = group.OrderBy(log => log.DateTime).ToList()
                })
                .ToListAsync(cancellationToken);

            var lastDaysSum = new List<GetGoodBadRatioResponse>();

            foreach (var day in lastXDaysMinutes)
            {
                var newDate = new GetGoodBadRatioResponse()
                {
                    Date = day.Date
                };

                newDate.CountGood = day.Logs.Count(log => log.Good);
                newDate.CountBad = day.Logs.Count(log => !log.Good);
                var sum = newDate.CountGood + newDate.CountBad;
                if (sum > 0)
                {
                    newDate.RatioGood = float.Round(100 * (float)newDate.CountGood / (float)sum, 2);
                    newDate.RatioBad = float.Round(100 * (float)newDate.CountBad / (float)sum, 2);
                }
                else
                {
                    newDate.RatioBad = 0;
                    newDate.RatioGood = 0;
                }
                lastDaysSum.Add(newDate);
            }
            string openaiKey = _configuration.GetValue<string>("OpenAi:Key");

            var goodAverageLegList = _applicationDbContext.SpinePostureDataLogs.Where(x => x.Good).Take(100);
            var goodAverageLeg = (float)goodAverageLegList.Count();
            if (goodAverageLeg!=0)
            {
                goodAverageLeg = goodAverageLegList.Average(x => x.LegDistance);
            }

            var badAverageLegList = _applicationDbContext.SpinePostureDataLogs.Where(x => !x.Good).Take(100);
            var badAverageLeg = (float)badAverageLegList.Count();
            if (badAverageLeg != 0)
            {
                badAverageLeg = badAverageLegList.Average(x => x.LegDistance);
            }

            var goodAverageBackList = _applicationDbContext.SpinePostureDataLogs.Where(x => x.Good).Take(100);
            var goodAverageBack = (float)goodAverageBackList.Count();
            if (goodAverageBack != 0)
            {
                goodAverageBack = goodAverageBackList.Average(x => x.UpperBackDistance);
            }

            var badAverageBackList = _applicationDbContext.SpinePostureDataLogs.Where(x => !x.Good).Take(100);
            var badAverageBack = (float)badAverageBackList.Count();
            if (badAverageBack != 0)
            {
                badAverageBack = badAverageBackList.Average(x => x.UpperBackDistance);
            }



            if (last5minutes.Count >= 6)
            {
                var badCount = last5minutes.Where(log => !log.Good).Count();
                var goodCount = last5minutes.Where(log => log.Good).Count();
                var sum = (float)(badCount + goodCount);

                var goodbadr = 100 * (float)(goodCount) / sum;
                var badgoodr = 100 * (float)(badCount) / sum;

                var top5 = last5minutes.OrderByDescending(o => o.DateTime).Take(5);
                var badCount5 = top5.Where(log => !log.Good).Count();
                var goodCount5 = top5.Where(log => log.Good).Count();
                var sum5 = (float)(badCount5 + goodCount5);

                var goodbadr5 = 100 * (float)(goodCount5) / sum5;
                var badgoodr5 = 100 * (float)(badCount5) / sum5;

                var averageLeg = last5minutes.Average(x => x.LegDistance);
                var averageBack = last5minutes.Average(x => x.UpperBackDistance);

               

                var openAi = new OpenAIAPI(new APIAuthentication(openaiKey));

                var conversation = openAi.Chat.CreateConversation();
                conversation.AppendUserInput($"In last 5 minutes, person was sitting and here are sensors data:" +
                                             $"Average back position from was {averageBack}cm, average leg distance was {averageLeg}cm" +
                                             $"and user was sitting correctly in {goodbadr}%, and in other times it was incorrect. " +
                                             $"Information with sitting data is from day {lastDaysSum[0].Date.ToString()}, and now in my time zone is {DatumVrijeme.ToString()}. You can say in sentence like today or yesterday, or somethin like that" +
                                             $"there was {lastDaysSum[0].RatioBad}% of bad postures for that day, and rest was good postures for that whole day." +
                                             $"Also, average leg position for last 5 minutes is: {averageLeg}cm, and average upper back position for last 5 minutes is {averageBack}cm"+
                                             $"Keep in mind that average good leg distance is {goodAverageLeg}cm, and average bad leg distance is {badAverageLeg}" +
                                             $"Keep in mind that average good upper back distance is {goodAverageBack}cm, and average bad leg distance is {badAverageBack}" +
                                             $"Write me a short message for user with this data" +
                                             $" who will see this message and warning, or information in one, two or three sentence. " +
                                             $"Add some data and say what user needs to do to improve it (if it's bad) and how it can affect" +
                                             $"his back pain." +
                                             $"his back pain. Say it in format of just two or three sentences, without dear user or something like that, round this average distances to 1 decimale, for example 15.1" +
                                             $"and say possible consquences, what to do to improve it (it it's bad). " +
                                             $"Start sentence like: your average back distance in last 5 minutes was so and so, and for leg distance similarly write" +
                                             $"Dont start sentence saying sensor data." +
                                             $"Also, try to give as precise data and advices and as new as possible, and as original as possible." +
                                             $"Also, if {lastDaysSum[0].Date.ToString()} is not today, you can say yesterday or two days ago, or something like that" +
                                             $"Also, don't say time, like in hours:minutes, only date in format: 06-Feb-24");
                var response = await conversation.GetResponseFromChatbotAsync();

                return Ok(new OpenAiResponse()
                {
                    Message = response
                });
            }

            var openAii = new OpenAIAPI(new APIAuthentication(openaiKey));

            var lastDateOfData = await _applicationDbContext.SpinePostureDataLogs
                .Where(x => x.ChairId == chairId)
                .OrderByDescending(x => x.DateTime)
                .FirstOrDefaultAsync(cancellationToken);

            if (lastDateOfData != null)
            {
                var lastDate = lastDateOfData.DateTime.Date;

                var averageBackPerson1 = _applicationDbContext.SpinePostureDataLogs
                    .Where(x => x.ChairId == chairId && x.DateTime.Date == lastDate)
                    .ToList();

                var averageBackPerson = _applicationDbContext.SpinePostureDataLogs
                .Where(x => x.ChairId == chairId && x.DateTime.Date==lastDate)
                .Average(x => x.UpperBackDistance);


                var conversationn = openAii.Chat.CreateConversation();
                
                conversationn.AppendUserInput($"Information with sitting data is from data {lastDaysSum[0].Date.ToString()}, and now in my time zone is {DatumVrijeme.ToString()}. You can say in sentence like today or yesterday, or somethin like that" +
                                              $"there was {lastDaysSum[0].RatioBad}% of bad postures for {lastDaysSum[0].Date.ToString()}, and rest was good postures, for that day." +
                                              $"Also, average back distance for this person is {GetRound(averageBackPerson)} cm, whereas average of good upper back distance is {GetRound(goodAverageBack)} cm" +
                                              $"Write me a short message for user with this data" +
                                              $" who will see this message and warning, or information in one, two or three sentence. " +
                                              $"Add some data and say what user needs to do to improve it (if it's bad) and how it can affect" +
                                              $"his back pain." +
                                              $"Say it in format of just two or three sentences, without dear user or something like that" +
                                              $"and say possible consquences, what to do to improve it (it it's bad). " +
                                              $"Start sentence like: Your percentage of bad postures today, yesterday, or something like this was so and so" +
                                              $"Dont start sentence saying sensor data" +
                                              $"Also, try to give as precise data and advices and as new as possible, and as original as possible" +
                                              $"Also, don't say time, like in hours:minutes, only date in format: 06-Feb-24");
                
                var responsee = await conversationn.GetResponseFromChatbotAsync();
                
                return Ok(new OpenAiResponse()
                {
                    Message = responsee
                });
            }

            

            return Ok(new OpenAiResponse() { Message = "" });

            return Ok(new GetWarningResponse()
            {
                GoodBadRatio = 0,
                BadGoodRatio = 0,
                BadGoodRatio5 = 0,
                BadCount5 = 0,
                GoodCount5 = 0,
                GoodBadRatio5 = 0,
                BadCount = 0,
                GoodCount = 0,
            });
           

            return Ok();
        }

        public static float GetRound(float number)
        {
            return float.Round(number, 2);
        }
    }   

}
