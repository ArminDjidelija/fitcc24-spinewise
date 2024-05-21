using Microsoft.AspNetCore.Mvc;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Models;
using System.Globalization;

namespace SpineWise.Web.Endpoints.ChairManagement.Edit
{
    [MyAuthorization("superadmin")]
    [Route("chair")]
    public class EditChairEndpoint:MyBaseEndpoint<EditChairRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EditChairEndpoint(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        [HttpPut("edit")]
        public override async Task<ActionResult<NoResponse>> Action([FromBody]EditChairRequest request, CancellationToken cancellationToken = default)
        {
            var chair = await _applicationDbContext
                .Chairs
                .FindAsync(request.Id);
            if (chair == null)
            {
                return BadRequest("Bad chair ID");
            }
            chair.Naziv=request.Naziv;
            chair.ChairModelId=request.Id;
            chair.SerialNumber=request.SerialNumber;
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok(new NoResponse());
        }
    }
}
