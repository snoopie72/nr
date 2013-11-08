using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NrDataLib.Model
{
    public class RaceInstance
    {
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public DateTime Dato { get; set; }
        public virtual Race Race {get; set; }
    }
}
