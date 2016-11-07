using ServiceBus;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServiceBusTests
{
    [TestClass]
    public class ProductServiceTests
    {
        ServiceBusTests.ProductService.ProductServiceClient client = new ProductService.ProductServiceClient();

        [TestMethod]
        public void GetProductTest()
        {
            
            var productId = new Guid("afe38a9e-9faf-411f-a94b-23d377b473b4");

            var product = client.GetProduct(productId);

            Assert.IsTrue(product.Result.ResultType == ProductService.ResultType.Success);
        }

        [TestMethod]
        public void GetAllProductsTest()
        {
            var product = client.GetAllProducts();

            Assert.IsTrue(product.Result.ResultType == ProductService.ResultType.Success);
        }


        [TestMethod()]
        public void AddProductTest()
        {
            var guid = Guid.NewGuid();
            var name = "Product_" + guid.ToString().Substring(0, 5);
            var price = 17.221;
            var headliner = true;

            //var productType = new ServiceBus.ProductType() {};

            var result = client.AddProduct(name, price, guid, 1, headliner);
            Assert.IsTrue(result.ResultType == ProductService.ResultType.Success);
        }

    }
}
