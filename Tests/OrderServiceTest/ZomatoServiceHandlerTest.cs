using BLL.OrderService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace OrderServiceTest
{
    [TestClass]
    public class ZomatoServiceHandlerTest
    {
        [TestMethod]
        public void HandleOrderCheckDivisionPaidPriceByQuantity()
        {
            var zomatoServiceHandler = new ZomatoServiceHandler();
            var expectedJson = JObject.Parse(
                @"{
                    ""orderNumber"": ""1"",
                    ""products"": [
                      {
                        ""id"": ""1"",
                        ""name"": ""null"",
                        ""comment"": ""null"",
                        ""quantity"": ""2"",
                        ""paidPrice"": ""50"",
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
                        ""quantity"": ""2"",
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
            var actual = zomatoServiceHandler.HandleOrder(sourceOrderJson.ToString());
            Assert.AreEqual(expected, actual);
        }
    }
}
