using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iBudget.Models
{
    public class Message
    {
            [Key]
            public int ID { get; set; }
            public string AddedBy { get; set; }
            public string message { get; set; }
            public int GroupId { get; set; }
    }
}
