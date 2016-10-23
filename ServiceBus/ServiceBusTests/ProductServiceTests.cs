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
            var productId = new Guid("afe38a9e-9faf-411f-a94b-23d377b473b4");

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
