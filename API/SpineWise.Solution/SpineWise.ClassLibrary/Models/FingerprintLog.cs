using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpineWise.ClassLibrary.Models
{
    public class FingerprintLog
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime LogDateTime { get; set; }
        public bool Successful { get; set; }
    }
}
