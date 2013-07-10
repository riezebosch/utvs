using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockingDemo;
using Moq;
using System.Collections.Generic;

namespace MockingDemo.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void VerhoogSalaris_BasisScenario_GeeftNieuwSalarisTerug()
        {
            // Arrange
            var pa = new PersoneelsAdministratieMock();
            var target = new SalarisAdministratie(pa);

            // Act
            var result = target.Verhoog(1234, 75m);

            // Assert
            Assert.AreEqual(1075m, result);

            Assert.IsTrue(pa.IsSaved);
        }

        [TestMethod]
        public void VerhoogSalaris_BasisScenario_GeeftNieuwSalarisTerug_MetMockingFramework()
        {
            // Arrange
            //var pa = new PersoneelsAdministratieMock();
            var pa = new Mock<IPersoneelsAdministratie>();
            pa.Setup(m => m.Zoek(1234))
                .Returns(new Persoon 
                { 
                    Salaris = 1000, 
                    Adres = new Adres 
                    { 
                        Straat = "Jacobistraat", 
                        Nummer = 72 
                    }, 
                    Kinderen = new List<Persoon> 
                    { 
                        new Persoon 
                        { 
                            Naam = "Pietje" 
                        } 
                    } 
                });

            pa.Setup(m => m.Save()).Verifiable();
            
            var target = new SalarisAdministratie(pa.Object);

            // Act
            var result = target.Verhoog(1234, 75m);

            // Assert
            Assert.AreEqual(1075m, result);
            pa.Verify();
        }

        

        [TestMethod]
        public void HetVerschilTussenDoublesEnDecimals()
        {
            decimal a = 0.1m,
                b = 0.2m,
                c = a + b;

            Assert.AreEqual(0.3, c);
        }
    }
}
