using SpineWise.ClassLibrary.Models;

namespace SpineWise.Web.Helpers.Loggers
{
    public interface ISignInLogger
    {
        Task LogSignIn(UserAccount userAccount, bool successfully);
    }
}
