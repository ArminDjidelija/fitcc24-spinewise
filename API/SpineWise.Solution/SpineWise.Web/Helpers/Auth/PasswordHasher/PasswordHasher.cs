namespace SpineWise.Web.Helpers.Auth.PasswordHasher
{
    public class PasswordHasher
    {
        public static async Task<string> Hash(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
            return passwordHash;
        }

        public static async Task<bool> Verify(string passwordHash, string inputPassword)
        {
            var result = BCrypt.Net.BCrypt.EnhancedVerify(inputPassword, passwordHash);
            return result;
        }
    }
}
