using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Station
    {
        [Key]
        public string Name { get; set; }
        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public List<Line> Lines { get; set; }
    }
}