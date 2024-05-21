using Microsoft.AspNetCore.Mvc;

namespace SpineWise.Web.Helpers.Endpoint
{
    [ApiController]
    public abstract class MyBaseEndpoint<TRequest, TResponse> : ControllerBase
    {
        public abstract Task<ActionResult<TResponse>> Action(TRequest request, CancellationToken cancellationToken = default);
    }
}
