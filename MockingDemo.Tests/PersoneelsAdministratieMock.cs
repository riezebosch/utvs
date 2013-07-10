using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockingDemo.Tests
{
    class PersoneelsAdministratieMock : IPersoneelsAdministratie
    {
        public Persoon Zoek(int id)
        {
            return new Persoon
            {
                Salaris = 1000m
            };
        }

        public bool IsSaved { get; set; }

        public bool Save()
        {
            IsSaved = true;
            return true;
        }
    }
}
