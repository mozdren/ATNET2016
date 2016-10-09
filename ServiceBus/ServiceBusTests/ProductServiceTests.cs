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
            var productId = new Guid("50c750ea-f78b-467a-83cb-652a086f84d5");

            var product = client.GetProduct(productId);

            Assert.IsTrue(product.Result.ResultType == ProductService.ResultType.Success);
        }

        [TestMethod]
        public void GetAllProductsTest()
        {
            var client = new ProductService.ProductServiceClient();

            var product = client.GetAllProducts();

            Assert.IsTrue(product.Result.ResultType == ProductService.ResultType.Success);
        }
    }
}
