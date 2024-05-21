using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpineWise.ClassLibrary.Models
{
    public class SignInLog
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(UserAccount))]
        public int UserAccountID { get; set; }
        public UserAccount UserAccount { get; set; }
        public DateTime TimeOfSignIn { get; set; }
        //public string? IpAddress { get; set; }
        public bool SuccessfullSignIn { get; set; }
    }
}
