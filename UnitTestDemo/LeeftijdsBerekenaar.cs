using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestDemo
{
    public class LeeftijdsBerekenaar
    {
        public int Bereken(DateTime geboortedatum)
        {
            if (geboortedatum > DateTime.Now)
            {
                throw new ArgumentException("Geboortedatum ligt in de toekomst");
            }

            int result = DateTime.Now.Year - geboortedatum.Year;
            if (DateTime.Now.Month < geboortedatum.Month ||
                DateTime.Now.Month == geboortedatum.Month 
                && DateTime.Now.Day < geboortedatum.Day)
            {
                result--;
            }
            
            return result;
        }
    }
}
