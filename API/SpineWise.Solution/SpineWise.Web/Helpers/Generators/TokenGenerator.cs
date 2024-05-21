using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace SpineWise.Web.Helpers.Generators
{
    public class TokenGenerator
    {
        public static string GenerateSerialNumber(int size=10)
        {
            var charSet = "ABCDEFGHJKLMNOPQRSTUVWXYZ23456789";
            return RandomStringGenerator(charSet, size);
        }

        public static string GenerateToken(int size = 32)
        {
            var charSet = "abcdefghijklmnopqrstuvwxyz0123456789";
            return RandomStringGenerator(charSet, size);
        }
        public static string RandomStringGenerator(string charSet, int size)
        {
            var chars = charSet.ToCharArray();
            var data = new byte[1];
            #pragma warning disable SYSLIB0023 // Type or member is obsolete
            var crypto = new RNGCryptoServiceProvider();
            #pragma warning restore SYSLIB0023 // Type or member is obsolete
            crypto.GetNonZeroBytes(data);
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(size);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }

            return result.ToString();
        }
    }
}
