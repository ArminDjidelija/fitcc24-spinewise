using Microsoft.AspNetCore.Mvc;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Endpoint;

namespace SpineWise.Web.Endpoints.ChairManagement.GetById
{
    [Route("chair")]
    public class GetChairByIdEndpoint:MyBaseEndpoint<GetChairByIdRequest, GetChairByIdResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GetChairByIdEndpoint(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        [HttpGet("getbyid")]
        public override async Task<ActionResult<GetChairByIdResponse>> Action([FromQuery]GetChairByIdRequest request, CancellationToken cancellationToken = default)
        {
            var chair = await _applicationDbContext.Chairs.FindAsync(request.Id);
            if (chair == null)
            {
                return BadRequest("Wrong chair id!");
            }

            return Ok(chair);
        }
    }
}
