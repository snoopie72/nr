using System;
using System.ComponentModel.DataAnnotations;

namespace NRWeb.Model
{
    public class RaceTime
    {
        [Key]
        public int RaceTimeId { get; set; }
        public virtual RaceInstance RaceInstance { get; set; }
        public virtual User User { get; set; }
        public TimeSpan Time { get; set; }
        public byte[] Bilde { get; set; }
        public string Kommentar { get; set; }
    }
}
