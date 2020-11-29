using BLL.OrderService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace OrderServiceTest
{
    [TestClass]
    public class TalabatServiceHandlerTest
    {
        [TestMethod]
        public void HandleOrderChangePositiveSignPaidPrice()
        {
            var talabatServiceHandler = new TalabatServiceHandler();
            var expectedJson = JObject.Parse(
                @"{
                    ""orderNumber"": ""1"",
                    ""products"": [
                      {
                        ""id"": ""1"",
                        ""name"": ""null"",
                        ""comment"": ""null"",
                        ""quantity"": ""null"",
                        ""paidPrice"": ""-100"",
                        ""unitPrice"": ""null"",
                        ""remoteCode"": ""null"",
                        ""description"": ""null"",
                        ""vatPercentage"": ""null"",
                        ""discountAmount"": ""null""
                      }
                    ],
                    ""createdAt"": ""null""
                }");
            var sourceOrderJson = JObject.Parse(
                @"{
                    ""orderNumber"": ""1"",
                    ""products"": [
                      {
                        ""id"": ""1"",
                        ""name"": ""null"",
                        ""comment"": ""null"",
                        ""quantity"": ""null"",
                        ""paidPrice"": ""100"",
                        ""unitPrice"": ""null"",
                        ""remoteCode"": ""null"",
                        ""description"": ""null"",
                        ""vatPercentage"": ""null"",
                        ""discountAmount"": ""null""
                      }
                    ],
                    ""createdAt"": ""null""
                }");
            var expected = Regex.Replace(expectedJson.ToString(), @"[ \r\n\t]", "");
            var actual = talabatServiceHandler.HandleOrder(sourceOrderJson.ToString());
            Assert.AreEqual(expected, actual);
        }
    }
}
