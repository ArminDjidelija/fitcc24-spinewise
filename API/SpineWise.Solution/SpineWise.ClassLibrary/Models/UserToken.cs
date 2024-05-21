using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpineWise.ClassLibrary.Models
{
    public class UserToken
    {
        [Key]
        public int Id { get; set; }
        public string TokenValue { get; set; }
        [ForeignKey(nameof(UserAccount))]
        public int UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }
        public DateTime TimeOfRecording { get; set; }
        //public string? IpAddress { get; set; }
    }
}
