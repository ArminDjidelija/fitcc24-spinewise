using Microsoft.AspNetCore.Mvc;
using SpineWise.ClassLibrary.Models;
using SpineWise.Web.Data;
using SpineWise.Web.Helpers.Auth;
using SpineWise.Web.Helpers.Auth.PasswordHasher;
using SpineWise.Web.Helpers.Endpoint;
using SpineWise.Web.Helpers.Generators;
using SpineWise.Web.Helpers.Models;
using System.Diagnostics.Metrics;

namespace SpineWise.Web.Endpoints.GenerateData
{
    [MyAuthorization("superuser")]
    [Route("generatedata")]
    public class GenerateUsers : MyBaseEndpoint<NoRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GenerateUsers(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }

        [HttpGet("generateusers")]
        public override async Task<ActionResult<NoResponse>> Action([FromQuery]NoRequest request,
            CancellationToken cancellationToken = default)
        {
            int counter = 1;
            for (int i = 0; i < 3; i++)
            {
                var count = _applicationDbContext.Users.Count() + 1;
                var pass = $"user{counter}{counter + 1}{counter + 2}+-!";

                var password =await PasswordHasher.Hash(pass);
                var user = new User
                {
                    BirthDate = DateTime.Now,
                    DateOfCreation = DateTime.Now,
                    Email = $"user{count}@user",
                    Password = password,
                    FirstName = "userf" + count,
                    LastName = "userl" + count,
                    Username = "userf.userl" + count
                };
                _applicationDbContext.Users.Add(user);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
            }

            return Ok(new NoResponse());
        }
    }
}