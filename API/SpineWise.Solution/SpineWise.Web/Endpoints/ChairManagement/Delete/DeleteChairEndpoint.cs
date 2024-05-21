using Microsoft.AspNetCore.Mvc;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Models;

namespace SpineWise.Web.Endpoints.ChairManagement.Delete
{
    [MyAuthorization("superadmin")]
    [Route("chair")]
    public class DeleteChairEndpoint:MyBaseEndpoint<DeleteChairRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DeleteChairEndpoint(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        [HttpDelete("delete")]
        public override async Task<ActionResult<NoResponse>> Action([FromQuery]DeleteChairRequest request, CancellationToken cancellationToken = default)
        {
            var chair = await _applicationDbContext.Chairs.FindAsync(request.Id);
            if (chair == null)
            {
                return BadRequest("Wrong chair ID");
            }

            _applicationDbContext.Chairs.Remove(chair);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok(new NoResponse());
        }
    }
}
