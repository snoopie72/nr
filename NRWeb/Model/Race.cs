using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace NrDataLib.Model
{
    public class Race
    {
        public String Navn { get; set; }
        public String Beskrivelse { get; set; }
        public int Type { get; set; }
        public int Avstand { get; set; }
        public ICollection<RaceInstance> RaceInstances { get; set; } 
    }
}
