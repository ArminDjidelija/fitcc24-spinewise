using System.Text.Json.Serialization;
using SpineWise.ClassLibrary.Models;

namespace SpineWise.Web.Helpers.Auth
{
    public class MyAuthInfo
    {
        public UserToken? AuthUserToken { get; set; }

        public MyAuthInfo(UserToken? authUserToken)
        {
            this.AuthUserToken = authUserToken;
        }

        [JsonIgnore]
        public UserAccount? UserAccount => AuthUserToken?.UserAccount;
        //public UserToken? autentifikacijaToken { get; set; }

        public bool IsLoggedIn => UserAccount != null;
    }
}
