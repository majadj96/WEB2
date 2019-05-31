using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Vehicle
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDvehicle { get; set; }
        public string RegistrationPlates { get; set; }

        [ForeignKey("Line")]
        public string Number { get; set; }

        public Line Line { get; set; }
    }
}