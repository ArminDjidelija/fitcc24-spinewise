using SpineWise.ClassLibrary.Models;

namespace SpineWise.Web.Helpers.Loggers
{
    public interface ISignOutLogger
    {
        Task LogSignOut(UserAccount userAccount);
    }
}
