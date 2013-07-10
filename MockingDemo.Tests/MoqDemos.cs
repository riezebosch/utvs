using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockingDemo.Tests
{
    [TestClass]
    public class MoqDemos
    {

        [TestMethod]
        public void VerschillenInSetupBijMoq()
        {
            var mock = new Mock<IPersoneelsAdministratie>();
            mock.Setup(m => m.Zoek(12345)).Returns(new Persoon { Salaris = 0 });
            mock.Setup(m => m.Zoek(2)).Returns(new Persoon { Salaris = 1234564345 });
            mock.Setup(m => m.Zoek(4)).Throws<ArgumentException>();

            Console.WriteLine(mock.Object.Zoek(12345).Salaris);
            Console.WriteLine(mock.Object.Zoek(2).Salaris);

            try
            {
                mock.Object.Zoek(4);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
            }

            Console.WriteLine(mock.Object.Zoek(-1) == null);

            mock.Setup(m => m.Zoek(It.IsAny<int>())).Returns(new Persoon { Salaris = 55 });
            Console.WriteLine(mock.Object.Zoek(-1) == null);
        }

        [TestMethod]
        public void VerschilTussenStubsEnMocksMetMoq_StricktMock_SetupNietGebruikt()
        {
            var pa1 = new Mock<IPersoneelsAdministratie>(MockBehavior.Strict);
            pa1.Setup(m => m.Zoek(It.IsAny<int>()));

            // Verify faalt niet, ook al is de mock strict
            pa1.Verify();
        }

        [TestMethod]
        public void VerschilTussenStubsEnMocksMetMoq_StrictMock_SetupNietGebruiktMetVerifyAll()
        {
            var pa1 = new Mock<IPersoneelsAdministratie>(MockBehavior.Strict);
            pa1.Setup(m => m.Zoek(It.IsAny<int>()));

            try
            {
                pa1.VerifyAll();
                Assert.Fail("VerifyAll zou wel moeten falen, ongeacht of de mock strict of loose is");
            }
            catch (Exception)
            {
            }
        }

        [TestMethod]
        public void VerschilTussenStubsEnMocksMetMoq_LooseMock_SetupNietGebruiktMetVerifyAll()
        {
            var pa1 = new Mock<IPersoneelsAdministratie>(MockBehavior.Loose);
            pa1.Setup(m => m.Zoek(It.IsAny<int>()));

            try
            {
                pa1.VerifyAll();
                Assert.Fail("VerifyAll zou wel moeten falen, ongeacht of de mock strict of loose is");
            }
            catch (Exception)
            {
            }
        }

        [TestMethod]
        public void VerschilTussenStubsEnMocksMetMoq_LooseMock_SetupNietGebruiktMetVerifiableEnVerify()
        {
            var pa1 = new Mock<IPersoneelsAdministratie>(MockBehavior.Loose);
            pa1.Setup(m => m.Zoek(It.IsAny<int>()))
                .Verifiable();

            try
            {
                pa1.Verify();
                Assert.Fail("Verify zou moeten falen door de Verifiable, ongeacht of de mock strict of loose is");
            }
            catch (Exception)
            {
            }
        }
    }
}
