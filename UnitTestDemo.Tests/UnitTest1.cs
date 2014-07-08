using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestDemo;
using System.Web;
using Microsoft.QualityTools.Testing.Fakes;

namespace UnitTestDemo.Tests
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void GegevenDatLeeftijdAlIsGepasseerd_BerekenLeeftijdIsHuidigJaarMinGeboorteJaar()
        {
            // Arrange
            var target = new LeeftijdsBerekenaar();
            
            // Act
            int result = target.Bereken(new DateTime(1982, 05, 04));
            
            // Assert   
            Assert.AreEqual(32, result);
        }

        [TestMethod]
        public void ExtraLeeftijdControleren()
        {
            var target = new LeeftijdsBerekenaar();

            int result = target.Bereken(new DateTime(1981, 05, 04));

            Assert.AreEqual(33, result);
        }

        [TestMethod]
        public void GegevenLeeftijdNogNietGepaseerd_BerekendeLeeftijdMoetEenHogerZijn()
        {
            var target = new LeeftijdsBerekenaar();

            int result = target.Bereken(new DateTime(1982, 08, 08));

            Assert.AreEqual(31, result);
        }

        [TestMethod]
        public void GegevenLeeftijdHuidigeMaandMaarNogNietGepaseerd_BerekendeLeeftijdMoetEenHogerZijn()
        {
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2014, 7, 7);

                var target = new LeeftijdsBerekenaar();
                int result = target.Bereken(new DateTime(1982, 07, 08));
                Assert.AreEqual(31, result);
            }
        }

        [TestMethod]

        public void GeboorteDatumInDeToekomst_GooitFoutmelding()
        {
            try
            {
                new LeeftijdsBerekenaar().Bereken(new DateTime(2015, 1, 1));
                Assert.Fail("Dit punt had niet bereikt mogen worden. We verwachten een exception.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Geboortedatum ligt in de toekomst", ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GeboorteDatumInDeToekomst_GooitFoutmelding_MaarWeControlerenDeMeldingNiet()
        {
            new LeeftijdsBerekenaar().Bereken(new DateTime(2015, 1, 1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GeboorteDatumInDeToekomst_GooitFoutmelding_GecombineerdMetExpectedException()
        {
            try
            {
                new LeeftijdsBerekenaar().Bereken(new DateTime(2015, 1, 1));
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Geboortedatum ligt in de toekomst", ex.Message);
                throw;
            }
        }


        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", 
            "AgeCalculatorInput.csv", "AgeCalculatorInput#csv", DataAccessMethod.Random)]
        public void TestDeAgeCalculatorInEenKeerAlleScenariosMetEenDataSource()
        {
            var input = (DateTime)TestContext.DataRow[0];
            var expected = (int)TestContext.DataRow[1];

            var actual = new LeeftijdsBerekenaar().Bereken(input);
            Assert.AreEqual(expected, actual,(string)TestContext.DataRow[2]);
        }


        [TestMethod]
        public void TestContextDemo()
        {
            Console.WriteLine(TestContext.TestDir);
        }

        [Owner("PM Binnendienst")]
        [TestMethod]
        public void TestInternalMethod()
        {
            var c = new Class1();
            c.Execute();
        }


        [Ignore]
        [TestCategory("Load Test")]
        [TestCategory("Block Coverage")]
        [TestMethod]
        public void TestBlockCoverage_ContainsBug()
        {
            Foo(true, true);
            Foo(false, false);
        }

        private string Foo(bool p1, bool p2)
        {
            object result = default(string); // null
            if (p1)
            {
                result = "P1";
            };

            if (p2)
            {
                result = "P2";
            }

            return result.ToString();
        }

        [TestCategory("Load Test")]
        [TestMethod]
        public void TestRaceCondition()
        {
            HttpRuntime.Cache["MyKey"] = 6;

            // ...

            HttpRuntime.Cache["MyKey"] = 
                (int)HttpRuntime.Cache["MyKey"] + 5;

            // ...

            HttpRuntime.Cache.Remove("MyKey");
        }

        class Persoon
        {
            public DateTime Geboortedatum { get; set; }
            public string Name { get; set; }
        }

        class Student : Persoon
        {
            public Guid Studentnummer { get; set; }
        }

        [TestMethod]
        public void TestAsVsCast()
        {
            Persoon p = new Student { Geboortedatum = new DateTime(1982, 4, 5), Name = "Piet", Studentnummer = Guid.NewGuid() };
            Student s = (Student)p;

            object o = 5;
            //if (o.GetType() == typeof(Persoon))
            if (o is Persoon)
            {
                var student2 = (Persoon)o;
            }

            Persoon student4 = o as Persoon;

            int i = 5;
            //var student3 = (Student)i;
        }
    }
}
