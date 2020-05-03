using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using JokeGenerator;

namespace JokeGeneratorTests
{
    [TestClass]
    public class ConsolePrinterTest
    {
        ConsoleInterface console = new ConsoleInterface();

        [TestMethod]
        public void TestPrint()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                console.Print("This is a test");

                string expected = "This is a test\r\n";
                Assert.AreEqual<string>(expected, sw.ToString());
            }
        }

        [TestMethod]
        public void TestPrintList()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                console.Print(new string[] {"chicken", "beef", "turckey"});

                string expected = "- chicken\n- beef\n- turckey\r\n";
                Assert.AreEqual<string>(expected, sw.ToString());
            }
        }
    }
}
