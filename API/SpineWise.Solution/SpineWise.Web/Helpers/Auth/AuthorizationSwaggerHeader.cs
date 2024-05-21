using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SpineWise.Web.Helpers.Auth
{
    public class AuthorizationSwaggerHeader:IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "auth-token",
                In = ParameterLocation.Header,
                Description = "Auth-token:"
            });
        }
    }
}
