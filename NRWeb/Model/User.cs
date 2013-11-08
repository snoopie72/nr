﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NrDataLib.Model
{
    public class User
    {
        public string Name { get; set; }
        public string Adresse { get; set; }
        public DateTime Fodselsdato { get; set; }
        public string Postnr { get; set; }
        public string Poststed { get; set; }
        public DateTime RegistringsDato { get; set; }
        public int Kjonn { get; set; }
        public string Epost { get; set; }
        public string Passord { get; set; }
        public string Motto { get; set; }
        public byte[] Bilde { get; set; }
        public string Telefon { get; set; }

        public virtual ICollection<RaceTime> RaceTimes { get; set; }
    }
}