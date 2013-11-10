using System;
using System.ComponentModel.DataAnnotations;

namespace NRWeb.Model
{
    public class Kontigent
    {
        [Key]
        public int KontigentId { get; set; }
        public int Year { get; set; }

        public virtual User Medlem { get; set; }
        public float Sum { get; set; }
        public DateTime DatoBetalt { get; set; }


    }
}
