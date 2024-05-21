using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpineWise.ClassLibrary.Models
{
    public class UserActionLog
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(UserAccount))]
        public int UserAccountID { get; set; }
        public UserAccount UserAccount { get; set; }
        public string? QueryPath { get; set; }
        public string? PostData { get; set; }
        public DateTime Time { get; set; }
        //public string? IpAddress { get; set; }
        public string? ExceptionMessage { get; set; }
        public bool IsException { get; set; }

    }
}
