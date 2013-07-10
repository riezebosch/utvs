using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockingDemo.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<IPersoneelsAdministratie>()
                .To<PersoneelsAdministratie>();

            // self binding niet nodig: https://github.com/ninject/ninject/wiki/Dependency-Injection-With-Ninject#skipping-the-type-binding-bit--implicit-self-binding-of-concrete-types
            //kernel.Bind<SalarisAdministratie>()
            //    .To<SalarisAdministratie>();

            var sa = kernel.Get<SalarisAdministratie>();
            sa.Verhoog(1234, 75m);
        }
    }
}
