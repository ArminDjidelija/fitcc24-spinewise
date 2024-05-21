using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpineWise.ClassLibrary.Models
{
    public class User:UserAccount
    {
        [ForeignKey(nameof(Chair))]
        public int? ChairId { get; set; }
        public Chair? Chair { get; set; }
    }
}
