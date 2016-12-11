using ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharedLibs.DataContracts;
using System;



namespace ServiceBusTests
{
    [TestClass()]
    public class OrderServiceTests
    {
        [TestMethod()]
        public void GetOrderTest()
        {
            OrderService.OrderServiceClient client = new OrderService.OrderServiceClient();
            var orderRes = client.GetOrder(new Guid("insert guid string"));

            Assert.IsTrue(orderRes.Result.ResultType == SharedLibs.Enums.ResultType.Success);
        }

        [TestMethod()]
        public void GetAllOrdersTest()
        {
            OrderService.OrderServiceClient client = new OrderService.OrderServiceClient();
            var ordersRes = client.GetAllOrders();

            Assert.IsTrue(ordersRes.Result.ResultType == SharedLibs.Enums.ResultType.Success);
        }


        [TestMethod()]
        public void CreateNewOrderTest()
        {
            OrderService.OrderServiceClient client = new OrderService.OrderServiceClient();
            Guid id;
            var result = client.CreateNewOrder(out id);
            Assert.IsTrue(result.ResultType == SharedLibs.Enums.ResultType.Success);
        }

        [TestMethod()]
        public void AddBasketToOrderTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddUserToOrderTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddAddressToOrderTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddBillingInformationToOrderTest()
        {
            Assert.Fail();
        }


        [TestMethod()]
        public void AddOrderTest()
        {
            OrderService.OrderServiceClient client = new OrderService.OrderServiceClient();

            Basket basket = new Basket();
            User user = new User();
            Address deliveryAddress = new Address();
            BillingInformation billingInformation = new BillingInformation();
            OrderState orderState = new OrderState();

            var res = client.AddOrder(Guid.NewGuid(), null, null, null, null, Convert.ToDateTime("2.12.2016"), Convert.ToDateTime("2.12.2016"), null);
            Assert.IsTrue(res.ResultType == SharedLibs.Enums.ResultType.Success);
        }


        [TestMethod()]
        public void EditOrderTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ChangeOrderStateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteOrderTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateEmailTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SendEmailTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreatePDFDocumentTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetOrderStateTest()
        {
            var client = new OrderService.OrderServiceClient();
            Guid id;
            var result = client.GetOrderState(SharedLibs.Enums.OrderStateType.Created, out id);
            Assert.IsTrue(result.ResultType == SharedLibs.Enums.ResultType.Success);
        }
    }
}