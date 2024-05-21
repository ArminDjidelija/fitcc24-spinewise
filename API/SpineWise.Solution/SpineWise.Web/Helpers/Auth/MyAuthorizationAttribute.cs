using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SpineWise.Web.Services;

namespace SpineWise.Web.Helpers.Auth
{
    public class MyAuthorizationAttribute:TypeFilterAttribute
    {
        public string Roles { get; }
        public MyAuthorizationAttribute(string roles = "") : base(typeof(MyAuthorizationAsyncActionFilter)) //string roles=""
        {
            Roles = roles;
            Arguments = new object[] { roles }; // Dodajte Roles kao argument prilikom instanciranja filtera

        }
    }

    public class MyAuthorizationAsyncActionFilter : IAsyncActionFilter
    {
        private readonly string _role;

        public MyAuthorizationAsyncActionFilter(string role) //string role
        {
            _role = role;
        }
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authService = context.HttpContext.RequestServices.GetService<MyAuthService>()!;
            var actionLogService = context.HttpContext.RequestServices.GetService<MyActionLogService>()!;

            if (!authService.IsLoggedIn())
            {
                context.Result = new UnauthorizedObjectResult("You are not logged in!");
                return;
            }

            var role = _role.ToLower();

            var roleArraySplit = role.Split(',');

            //if (niz.Length>0 || rola == "" || rola == null)
            //{
            //    context.Result = new UnauthorizedObjectResult("Nema role");
            //    return;
            //}
            if (roleArraySplit.Length == 0 || roleArraySplit.Contains("everybody"))
            {
                await next();
                await actionLogService.Create(context.HttpContext);
                return;
            }

            var isSuperAdmin = await authService.IsSuperAdmin();
            if ((!roleArraySplit.Contains("superadmin") && isSuperAdmin))
            {
                context.Result = new UnauthorizedObjectResult("Not enough privileges");
                return;
            }
            var isUser = await authService.IsUser();
            if (!roleArraySplit.Contains("user") && isUser)
            {
                context.Result = new UnauthorizedObjectResult("Not enough privileges");
                return;
            }

            //MyAuthInfo myAuthInfo = authService.GetAuthInfo();

            await next();
            await actionLogService.Create(context.HttpContext);
        }
    }
}
