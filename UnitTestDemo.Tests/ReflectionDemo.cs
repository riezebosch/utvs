using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestDemo.Tests
{
    [TestClass]
    public class ReflectionDemo
    {
        class MyClass
        {
            public int MyProperty { get; set; }
            public string Name { get; set; }

            private Guid _id = Guid.NewGuid();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var c = new MyClass();
            c.MyProperty = 13;

            Type info = c.GetType();
            Type info2 = typeof(MyClass);

            object result = info.GetProperty("MyProperty").GetValue(c);
            Console.WriteLine(result);

            object id = info.GetField("_id", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(c);
            info.GetField("_id", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(c, null);
            Console.WriteLine(id);
        }
    }
}
