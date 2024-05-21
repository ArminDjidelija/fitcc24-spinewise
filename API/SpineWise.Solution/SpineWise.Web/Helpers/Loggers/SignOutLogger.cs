using SpineWise.ClassLibrary.Models;
using SpineWise.Web.Data;

namespace SpineWise.Web.Helpers.Loggers
{
    public class SignOutLogger:ISignOutLogger
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SignOutLogger(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task LogSignOut(UserAccount userAccount)
        {
            var signOutLog = new SignOutLog
            {
                UserAccountID = userAccount.Id,
                TimeOfSignOut = DateTime.Now
            };

            _dbContext.SignOutLogs.Add(signOutLog);
            await _dbContext.SaveChangesAsync();
        }
    }
}
