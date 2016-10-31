using System;
using System.ServiceModel;
using SharedLibs.DataContracts;

namespace ServiceBus
{
    /// <summary>
    /// This is a order service, which should serve as an entrypoint for everything that has
    /// something directly in common with orders.
    /// </summary>
    [ServiceContract]
    public interface IOrderService
    {
        /// <summary>
        /// Returns order with specific Guid
        /// </summary>
        /// <param name="guid">guid of a order</param>
        /// <returns>requested order</returns>
        [OperationContract]
        SharedLibs.DataContracts.Order GetOrder(Guid guid);


        /// <summary>
        /// Returns list of orders
        /// </summary>
        /// <returns>list of orders</returns>
        [OperationContract]
        SharedLibs.DataContracts.Orders GetAllOrders();


        /// <summary>
        /// Add new order to system
        /// </summary>
        /// <param name="guid">ID of a order</param>
        /// <param name="basket">Reference to basket object</param>
        /// <param name="address">Reference to address object</param>
        /// <param name="billingInformation">Reference to billingInformation object</param>
        /// <param name="orderDate">Order creation date</param>
        /// <param name="deliveryDate">Order delivery date</param>
        /// <returns>Result object</returns>
        [OperationContract]
        SharedLibs.DataContracts.Result AddOrder(Guid guid, Basket basket, Address address, BillingInformation billingInformation,
                                                 DateTime orderDate, DateTime deliveryDate);


        /// <summary>
        /// Add new order to system
        /// </summary>
        /// <param name="order">Order object</param>
        /// <returns>Result object</returns>
        [OperationContract(Name = "AddOrderByObject")]
        SharedLibs.DataContracts.Result AddOrder(SharedLibs.DataContracts.Order order);


        /// <summary>
        /// Editation of order
        /// </summary>
        /// <param name="guid">ID of a order</param>
        /// <param name="basket">Reference to basket object</param>
        /// <param name="address">Reference to address object</param>
        /// <param name="billingInformation">Reference to billingInformation object</param>
        /// <param name="deliveryDate">Order delivery date</param>
        /// <returns>New Order object</returns>
        [OperationContract]
        SharedLibs.DataContracts.Order EditOrder(Guid guid, Basket basket, Address address, BillingInformation billingInformation,
                                                 DateTime deliveryDate);


        /// <summary>
        /// Order state change
        /// </summary>
        /// <param name="guid">ID of a order</param>
        /// <param name="state">State of order</param>
        /// /// <returns>Result object</returns>
        [OperationContract]
        SharedLibs.DataContracts.Result ChangeOrderState(Guid guid, int state);
        // As state it'd be better to use enums - CANCEL, PAID, FINISHED
    }
}
