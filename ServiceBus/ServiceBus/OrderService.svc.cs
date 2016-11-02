using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using SharedLibs.DataContracts;

namespace ServiceBus
{
    public class OrderService : IOrderService
    {

        // Implementation and other parts - I will make an effort to do it per partes :-) KZ
        public SharedLibs.DataContracts.Order GetOrder(Guid guid)
        {
            return new SharedLibs.DataContracts.Order
            {
                Result = Result.Fatal("Not finish")
            };
        }


        public SharedLibs.DataContracts.Orders GetAllOrders()
        {
            return new SharedLibs.DataContracts.Orders
            {
                Result = Result.Fatal("Not finish")
            };
        }


        public Result AddOrder(Guid guid, Basket basket, Address address, BillingInformation billingInformation,
                                                 DateTime orderDate, DateTime deliveryDate)
        {
            return Result.Fatal("Not finish");
        }


        public SharedLibs.DataContracts.Result AddOrder(Order order)
        {
            return AddOrder(order.Id, order.Basket, order.DeliveryAddress, order.BillingInformation,
                            order.OrderDate, order.DeliveryDate);
        }


        public SharedLibs.DataContracts.Order EditOrder(Guid guid, Basket basket, Address address,
                                                        BillingInformation billingInformation, DateTime deliveryDate)
        {
            //****
            return new SharedLibs.DataContracts.Order()
            {
                Result = Result.Fatal("Not finish")
            };
        }


        public SharedLibs.DataContracts.Result ChangeOrderState(Guid guid, SharedLibs.Enums.OrderStateType stateType)
        {
            return Result.Fatal("Not finish");
        }


        public SharedLibs.DataContracts.Result SendEmail(string user, string emailText)
        {
            return Result.Fatal("Not finish");
        }


        public SharedLibs.DataContracts.Result CreateInvoice(string user, Order order)
        {
            return Result.Fatal("Not finish");
        }
    }
}
