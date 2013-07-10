using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MockingDemo
{
    public class Persoon
    {
        public decimal Salaris { get; set; }

        public Adres Adres { get; set; }

        public string Naam { get; set; }

        public List<Persoon> Kinderen { get; set; }
    }
}
