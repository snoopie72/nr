using System;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace NRWeb.Model
{
    public class RaceInstance
    {
        [Key]
        public int RaceInstanceId { get; set; }
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public DateTime Dato { get; set; }
        public virtual Race Race {get; set; }
    }
}
