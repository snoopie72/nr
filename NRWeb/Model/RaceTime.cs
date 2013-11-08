using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NrDataLib.Model
{
    public class RaceTime
    {
        public virtual RaceInstance RaceInstance { get; set; }
        public virtual User User { get; set; }
        public DateTime Time { get; set; }
        public byte[] Bilde { get; set; }
        public string Kommentar { get; set; }
    }
}
