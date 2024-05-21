using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpineWise.Web.Data;
using SpineWise.Web.Endpoints.ChairManagement.GetAll;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Models;

namespace SpineWise.Web.Endpoints.ChairManagement.Get
{
    [MyAuthorization("superadmin")]
    [Route("chair")]
    public class GetAllChairEndpoint:MyBaseEndpoint<NoRequest, GetAllChairResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GetAllChairEndpoint(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        [HttpGet("getall")]
        public override async Task<ActionResult<GetAllChairResponse>> Action([FromQuery]NoRequest request, CancellationToken cancellationToken = default)
        {
            var chairs = await _applicationDbContext
                .Chairs
                .Select(x => new GetAllChairResponse()
                {
                    Id = x.Id,
                    SerialNumber = x.SerialNumber,
                    DateOfCreating = x.DateOfCreating,
                    ChairModel = x.ChairModel,
                })
                .ToListAsync(cancellationToken);

            return Ok(chairs);
        }
    }
}
