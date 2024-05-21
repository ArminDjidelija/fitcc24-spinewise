using Microsoft.AspNetCore.Mvc;
using SpineWise.ClassLibrary.Models;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Models;

namespace SpineWise.Web.Endpoints.ChairModelManagement.Add
{
    [MyAuthorization("superadmin")]
    [Route("chairmodel")]
    public class AddChairModelEndpoint:MyBaseEndpoint<AddChairModelRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AddChairModelEndpoint(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        [HttpPost("add")]
        public override async Task<ActionResult<NoResponse>> Action([FromBody]AddChairModelRequest request, CancellationToken cancellationToken = default)
        {
            var newObj = new ChairModel()
            {
                DateOfCreating = request.DateOfAdding,
                Name = request.Name,
            };

            _applicationDbContext.ChairModels.Add(newObj);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return Ok(new NoResponse() { });
        }
    }
}
