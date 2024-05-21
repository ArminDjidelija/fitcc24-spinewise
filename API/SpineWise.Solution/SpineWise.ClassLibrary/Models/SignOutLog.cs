using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SpineWise.ClassLibrary.Models
{
    public class SignOutLog
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(UserAccount))]
        public int UserAccountID { get; set; }
        public UserAccount UserAccount { get; set; }
        public DateTime TimeOfSignOut { get; set; }
        //public string? IpAddress { get; set; }
    }
}
