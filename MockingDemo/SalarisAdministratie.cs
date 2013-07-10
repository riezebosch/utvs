using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MockingDemo
{
    public class SalarisAdministratie
    {
        private IPersoneelsAdministratie _pa;

        //public SalarisAdministratie() :
        //    this(new PersoneelsAdministratie())
        //{
        //}

        public SalarisAdministratie(IPersoneelsAdministratie pa)
        {
            _pa = pa;
        }
        public decimal Verhoog(int id, decimal verhoging)
        {
            Persoon p = _pa.Zoek(id);
            p.Salaris += verhoging;

            _pa.Save();

            return p.Salaris;
        }
    }
}
