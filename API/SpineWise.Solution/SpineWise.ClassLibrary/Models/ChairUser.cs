using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpineWise.ClassLibrary.Models
{
    public class ChairUser
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Chair))]
        public int ChairId { get; set; }
        public Chair Chair { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime DateOfAcquiring { get; set; }
    }
}
