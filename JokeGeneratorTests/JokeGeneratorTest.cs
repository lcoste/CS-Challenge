using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;

namespace JokeGeneratorTests
{
    [TestClass]
    public class JokeGeneratorTest
    {
        JokeGenerator.JokeGenerator generator = new JokeGenerator.JokeGenerator();

        [TestMethod]
        public void TestResetState()
        {
            generator.ResetState();

            Assert.AreEqual<JokeGenerator.JokeGenerator.State>(JokeGenerator.JokeGenerator.State.Main, generator.state);
        }
    }
}
