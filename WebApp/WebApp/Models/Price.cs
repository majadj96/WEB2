﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Price
    {
        [Key]
        public int IDPrice { get; set; }

        public int IDtypeOfTicket { get; set; }

        public double Value { get; set; }

        [JsonIgnore]
        public List<PriceList> PriceLists { get; set; }
        
     

    }
}