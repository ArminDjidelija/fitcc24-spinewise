using Microsoft.AspNetCore.Mvc.Filters;

namespace SpineWise.Web.Helpers.Auth.LogCustomAuth
{
    public class DataLogHeaderAttribute:ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("security-token", "sLNHXBqSZkW3f6dWu5d0liypqSC93kQP");
            base.OnResultExecuting(context);
        }
    }
}
