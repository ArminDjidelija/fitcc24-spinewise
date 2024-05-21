using Microsoft.AspNetCore.Mvc;
using SpineWise.Web.Data;
using SpineWise.Web.Endpoints.ChairManagement.Edit;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Models;

namespace SpineWise.Web.Endpoints.ChairManagement.UserEdit
{
    public class ChairEditEndpoint:MyBaseEndpoint<ChairEditRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ChairEditEndpoint(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        [HttpPut("chair")]
        public override async Task<ActionResult<NoResponse>> Action([FromBody] ChairEditRequest request, CancellationToken cancellationToken = default)
        {
            var chair = await _applicationDbContext
                .Chairs
                .FindAsync(request.Id);
            if (chair == null)
            {
                return BadRequest("Bad chair ID");
            }
            chair.Naziv = request.Naziv;
            if (request.Delay < 10 || request.Delay > 60)
                return BadRequest("Interval mora biti između 10 i 60 sekundi.");
            chair.Delay=request.Delay;
            chair.SendData = request.SaljiPodatke;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return Ok(new NoResponse());
        }
    }
}
