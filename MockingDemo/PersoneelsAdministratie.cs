using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MockingDemo
{
    public class PersoneelsAdministratie : MockingDemo.IPersoneelsAdministratie
    {
        public Persoon Zoek(int id)
        {
            throw new NotImplementedException();
        }


        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
