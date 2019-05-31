using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDticket { get; set; }

        public DateTime BoughtTime { get; set; }
        public DateTime CheckIn { get; set; }


        [ForeignKey("TypeOfTicket")]
        public int IDtypeOfTicket { get; set; }

        public TypeOfTicket TypeOfTicket { get; set; }


        // [ForeignKey("ApplicationUser")]
        public string UserName { get; set; } 

        // public ApplicationUser ApplicationUser { get; set; }
    }
}