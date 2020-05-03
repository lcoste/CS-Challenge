using Microsoft.VisualStudio.TestTools.UnitTesting;
using JokeGenerator;
using System.Collections.Generic;

namespace JokeGeneratorTests
{
    [TestClass]
    public class JsonFeedTest
    {
        JsonFeed feed = new JsonFeed();

        [TestMethod]
        public void TestGenerateUrl()
        {
            string actual = JsonFeed.GenerateUrl("host.com", "endpoint/api", null);
            Assert.AreEqual<string>("host.com/endpoint/api", actual);

            actual = JsonFeed.GenerateUrl("host.com", "/endpoint/api", null);
            Assert.AreEqual<string>("host.com/endpoint/api", actual);

            actual = JsonFeed.GenerateUrl("host.com/", "endpoint/api", null);
            Assert.AreEqual<string>("host.com/endpoint/api", actual);

            actual = JsonFeed.GenerateUrl("host.com/", "/endpoint/api", null);
            Assert.AreEqual<string>("host.com/endpoint/api", actual);

            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "key1", "value1" }
            };
            actual = JsonFeed.GenerateUrl("host.com", "endpoint/api", parameters);
            Assert.AreEqual<string>("host.com/endpoint/api?key1=value1", actual);

            parameters = new Dictionary<string, string>
            {
                { "key1", "value1" },
                { "key2", "value2" }
            };
            actual = JsonFeed.GenerateUrl("host.com", "endpoint/api", parameters);
            Assert.AreEqual<string>("host.com/endpoint/api?key1=value1&key2=value2", actual);

            parameters = new Dictionary<string, string>
            {
                { "key 1", "value 1!" }
            };
            actual = JsonFeed.GenerateUrl("host.com", "endpoint/api", parameters);
            Assert.AreEqual<string>("host.com/endpoint/api?key%201=value%201%21", actual);
        }
    }
}