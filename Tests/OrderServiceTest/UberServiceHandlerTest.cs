using BLL.OrderService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;

namespace OrderServiceTest
{
    [TestClass]
    public class UberServiceHandlerTest
    {
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void HandleOrderChangePositiveSignPaidPrice()
        {
            var uberServiceHandler = new UberServiceHandler();
            uberServiceHandler.HandleOrder("");
        }
    }
}
