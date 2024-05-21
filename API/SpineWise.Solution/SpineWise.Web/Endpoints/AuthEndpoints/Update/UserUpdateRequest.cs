namespace SpineWise.Web.Endpoints.AuthEndpoints.Update
{
    public class UserUpdateRequest
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; } = "";
        public string NewPassword { get; set; } = "";
        public string NewPasswordConfirm { get; set; } = "";
    }
}
