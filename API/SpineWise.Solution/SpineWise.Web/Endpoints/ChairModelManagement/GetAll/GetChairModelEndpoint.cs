using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Models;

namespace SpineWise.Web.Endpoints.ChairModelManagement.Get
{
    [Route("chairmodel")]
    public class GetChairModelEndpoint:MyBaseEndpoint<NoResponse, GetChairModelResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GetChairModelEndpoint(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        [HttpGet("getall")]
        public override async Task<ActionResult<GetChairModelResponse>> Action([FromQuery]NoResponse request, CancellationToken cancellationToken = default)
        {
            var chairmodels = await _applicationDbContext
                .ChairModels
                .Select(x=>new GetChairModelResponse
                {
                    Name = x.Name,
                    Id = x.Id,
                    DateOfCreating = x.DateOfCreating
                })
                .ToListAsync(cancellationToken);

            return Ok(chairmodels);
        }
    }
}
