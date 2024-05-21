using SpineWise.ClassLibrary.Models;
using SpineWise.Web.Data;

namespace SpineWise.Web.Helpers.Loggers
{
    public class SignInLogger:ISignInLogger
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SignInLogger(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = applicationDbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task LogSignIn(UserAccount userAccount, bool successfully)
        {
            var signInLog = new SignInLog
            {
                SuccessfullSignIn = successfully,
                UserAccountID = userAccount.Id,
                TimeOfSignIn = DateTime.Now
            };

            _dbContext.SignInLogs.Add(signInLog);
            await _dbContext.SaveChangesAsync();
        }
    }
}
