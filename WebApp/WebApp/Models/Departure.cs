using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Departure
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDDeparture { get; set; }

        [ForeignKey("Day")]
        public int IDDay { get; set; }

        public Day Day { get; set; }

        public DateTime Time { get; set; }

        [JsonIgnore]
        public virtual List<Line> Lines { get; set; }

     
    }
}