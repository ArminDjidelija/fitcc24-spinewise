using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpineWise.ClassLibrary.Models;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Generators;
using SpineWise.Web.Helpers.Models;

namespace SpineWise.Web.Endpoints.ChairManagement.Add
{
    [MyAuthorization("superadmin")]
    [Route("chair")]
    public class AddChairEndpoint:MyBaseEndpoint<AddChairRequest, AddChairResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AddChairEndpoint(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        [HttpPost("add")]
        public override async Task<ActionResult<AddChairResponse>> Action([FromBody]AddChairRequest request, CancellationToken cancellationToken = default)
        {
            var serialNumber=TokenGenerator.GenerateSerialNumber(8);
            var chair = new Chair
            {
                SerialNumber = serialNumber,
                DateOfCreating = DateTime.Now,
                ChairModelId = request.ChairModelId
            };

            _applicationDbContext.Chairs.Add(chair);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var addedChair = await _applicationDbContext.Chairs.Where(x => x.SerialNumber == serialNumber)
                .FirstOrDefaultAsync(cancellationToken);

            return Ok(new AddChairResponse
            {
                id = addedChair.Id,
                serialNumber = serialNumber
            });
        }
    }
}
