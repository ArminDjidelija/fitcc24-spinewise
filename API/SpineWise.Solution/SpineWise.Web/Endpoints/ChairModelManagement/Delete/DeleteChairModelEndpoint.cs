using Microsoft.AspNetCore.Mvc;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Models;

namespace SpineWise.Web.Endpoints.ChairModelManagement.Delete
{
    [Route("chairmodel")]
    public class DeleteChairModelEndpoint:MyBaseEndpoint<int, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DeleteChairModelEndpoint(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        [HttpDelete("delete")]
        public override async Task<ActionResult<NoResponse>> Action([FromQuery] int id, CancellationToken cancellationToken = default)
        {
            var chair = await _applicationDbContext.ChairModels.FindAsync(id);
            if (chair == null)
            {
                return BadRequest("Wrong chair id!");
            }

            _applicationDbContext.ChairModels.Remove(chair);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}
