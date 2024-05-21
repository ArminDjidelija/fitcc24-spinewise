using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpineWise.ClassLibrary.Models
{
    public class SpineWiseDataLog
    {
        [Key]
        public int Id { get; set; }

        public float LegDistance { get; set; }
        public float LumbarBackDistance { get; set; } //lower back
        public float ThoracicBackDistance { get; set; } //upper back
        public DateTime LogDateTime { get; set; }

        [ForeignKey(nameof(Chair))]
        public int ChairId { get; set; }
        public Chair Chair { get; set; }


        //[ForeignKey(nameof(User))]
        //public int UserId { get; set; }
        //public User User { get; set; }
    }
}
