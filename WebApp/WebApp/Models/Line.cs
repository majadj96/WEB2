using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Line
    {

        [Key]
        public string Number { get; set; }

        public int IDtypeOfLine { get; set; }

        public List<Station> Stations { get; set; }

        public virtual List<Departure> Departures { get; set; }

    }
}