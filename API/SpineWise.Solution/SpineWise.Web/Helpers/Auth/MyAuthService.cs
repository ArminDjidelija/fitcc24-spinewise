using Microsoft.EntityFrameworkCore;
using SpineWise.ClassLibrary.Models;
using SpineWise.Web.Data;

namespace SpineWise.Web.Helpers.Auth
{
    public class MyAuthService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyAuthService(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _applicationDbContext = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public MyAuthInfo GetAuthInfo()
        {
            string? authToken = _httpContextAccessor.HttpContext!.Request.Headers["auth-token"];

            UserToken? autentifikacijaToken = _applicationDbContext.UserTokens
                .Include(x => x.UserAccount)
                .SingleOrDefault(x => x.TokenValue == authToken);

            return new MyAuthInfo(autentifikacijaToken);
        }
        public bool IsLoggedIn()
        {
            return GetAuthInfo().IsLoggedIn;
        }

        public async Task<bool> IsSuperAdmin()
        {
            var userAccount = GetAuthInfo().UserAccount;
            if (userAccount == null)
                return false;
            var admin = await _applicationDbContext.SuperAdmins.FindAsync(userAccount.Id);
            if (admin == null)
                return false;

            //_applicationDbContext.UpozorenjeKorisnickiRacun.Add(new UpozorenjeKorisnickiRacun
            //    { KorisnickiRacun = userAccount, TipProblema = "Administrator nije oznacen sa isAdmin" });
            //await _applicationDbContext.SaveChangesAsync();
            //return false;

            return true;
        }

        public async Task<bool> IsUser()
        {
            var userAccount = GetAuthInfo().UserAccount;
            if (userAccount == null)
                return false;
            var user = await _applicationDbContext.Users.FindAsync(userAccount.Id);
            if (user == null)
                return false;

            return true;
        }
    }
}
