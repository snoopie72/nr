using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace NrDataLib.Model
{
    public class Kontigent
    {
        public int Year { get; set; }

        public virtual User Medlem { get; set; }
        public float Sum { get; set; }
        public DateTime DatoBetalt { get; set; }


    }
}
