using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace NRWeb.Model
{
    public class Race
    {
        [Key]
        public int RaceId { get; set; }
        public String Navn { get; set; }
        public String Beskrivelse { get; set; }
        public int Type { get; set; }
        public int Avstand { get; set; }
        public ICollection<RaceInstance> RaceInstances { get; set; } 
    }
}
