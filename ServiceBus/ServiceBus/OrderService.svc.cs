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
        // At this time is not created table order

        /// <summary>
        /// Method returns order according requested ID number
        /// </summary>
        /// <param name="guid">Order ID number</param>
        /// <returns>Order object</returns>
        public SharedLibs.DataContracts.Order GetOrder(Guid guid)
        {
            try
            {
                using (var context = new ServiceBusDatabaseEntities())
                {
                    /*
                    var order = context.Orders.FirstOrDefault(o => o.ID == guid);

                    if (order == null)
                    {
                        return new SharedLibs.DataContracts.Order
                        {
                            Result = SharedLibs.DataContracts.Result.ErrorFormat("Requested order ID number {0} was not found.", guid)
                        };
                    }

                    return new SharedLibs.DataContracts.Order
                    {
                        Id = guid;
                        Basket = order.Basket;
                        DeliveryAddress = order.DeliveryAddress;
                        BillingInformation = order.BillingInformation;
                        OrderDate = order.OrderDate;
                        DeliveryDate = order.DeliveryDate;
                        OrderState = order.OrderState;
                        Result = SharedLibs.DataContracts.Result.SuccessFormat("Requested order ID number {0} was found.", guid);

                    };
                    */
                }
            }
            catch (Exception exception)
            {
                /*
                return new SharedLibs.DataContracts.Order
                {
                    Result = SharedLibs.DataContracts.Result.FatalFormat("In method OrderService.GetOrder was thrown exception: {0}", exception.Message)
                };
                */
            }

            // delete this code after finishing implementation method GetOrder 
            return new SharedLibs.DataContracts.Order
            {
                Result = Result.Fatal("Not finish")
            };
        }


        /// <summary>
        /// Method returns list of orders in collection
        /// </summary>
        /// <returns>List of orders</returns>
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
            return new SharedLibs.DataContracts.Order()
            {
                Result = Result.Fatal("Not finish")
            };
        }


        public SharedLibs.DataContracts.Result ChangeOrderState(Guid guid, SharedLibs.Enums.OrderStateType orderState)
        {
            try
            {
                using (var context = new ServiceBusDatabaseEntities())
                {
                    /*
                    var order = context.Orders.FirstOrDefault(o => o.ID == guid);

                    if (order == null)
                    {
                        return SharedLibs.DataContracts.Result.ErrorFormat("Requested order ID number {0} was not found.", guid);
                    }
                    else
                    {
                        order.OrderState = orderState;
                        return SharedLibs.DataContracts.Result.SuccessFormat("State of requested order ID number {0} was changed.", guid);
                    */
                }
            }
            catch (Exception exception)
            {
                /*               
                return SharedLibs.DataContracts.Result.FatalFormat("In method OrderService.GetOrder was thrown exception: {0}", exception.Message)
                */
            }

            // delete this code after finishing implementation method ChangeOrderState
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

        public SharedLibs.DataContracts.Result DeleteOrder(Guid guid)
        {
            return Result.Fatal("Not finish");
        }

        public SharedLibs.DataContracts.Result SendEmail(User user, string emailText)
        {
            return Result.Fatal("Not finish");
        }

        public SharedLibs.DataContracts.Result CreateInvoice(User user, Order order)
        {
            return Result.Fatal("Not finish");
        }
    }
}
