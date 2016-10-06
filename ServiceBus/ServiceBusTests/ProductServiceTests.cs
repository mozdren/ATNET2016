using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServiceBusTests
{
    [TestClass]
    public class ProductServiceTests
    {
        [TestMethod]
        public void GetProductTest()
        {
            var client = new ProductService.ProductServiceClient();
            var productId = Guid.Empty;

            var product = client.GetProduct(productId);

            Assert.IsTrue(product.Result.ResultType == ProductService.ResultType.Success); // unless the implementation is done, this test will fail
        }
    }
}
