using System.ComponentModel.DataAnnotations;

namespace SpineWise.ClassLibrary.Models
{
    public class ChairModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreating { get; set; }
    }
}
