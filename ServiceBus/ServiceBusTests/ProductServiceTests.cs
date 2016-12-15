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
            var productId = new Guid("c5b87c07-a019-471c-9c77-4f1380950f73");

            var product = client.GetProduct(productId);

            Assert.IsTrue(product.Result.ResultType == SharedLibs.Enums.ResultType.Success);
        }

        [TestMethod]
        public void GetAllProductsTest()
        {
            var client = new ProductService.ProductServiceClient();

            var product = client.GetAllProducts();

            Assert.IsTrue(product.Result.ResultType == SharedLibs.Enums.ResultType.Success);
        }
    }
}
