using System;
namespace MockingDemo
{
    public interface IPersoneelsAdministratie
    {
        Persoon Zoek(int id);

        bool Save();
    }
}
