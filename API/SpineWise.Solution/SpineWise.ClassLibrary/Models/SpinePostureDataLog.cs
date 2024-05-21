using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpineWise.ClassLibrary.Models
{
    public class SpinePostureDataLog
    {
        [Key]
        public int Id { get; set; }
        public float UpperBackDistance { get; set; }
        public float LegDistance { get; set; }
        public bool PressureSensor1 { get; set; }
        public bool PressureSensor2 { get; set; }
        public bool PressureSensor3 { get; set; }
        public bool PressureSensor4 { get; set; }
        public DateTime DateTime { get; set; }
        [ForeignKey(nameof(Chair))]
        public int ChairId { get; set; }
        public Chair Chair { get; set; }
        public bool Good{ get; set; }
    }
}
