using System;
using System.ServiceModel;
using SharedLibs.DataContracts;
using SharedLibs.Enums;

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
        /// Delete order with specific Guid
        /// </summary>
        /// <param name="guid">guid of a order</param>
        /// <returns>Result object</returns>
        [OperationContract]
        SharedLibs.DataContracts.Result DeleteOrder(Guid guid);


        /// <summary>
        /// Order state change
        /// </summary>
        /// <param name="guid">ID of a order</param>
        /// <param name="state">State of order</param>
        /// /// <returns>Result object</returns>
        [OperationContract]
        SharedLibs.DataContracts.Result ChangeOrderState(Guid guid, SharedLibs.Enums.OrderStateType orderState);


        /// <summary>
        /// Send e-mail to client
        /// </summary>
        /// <param name="user">Reference to user</param>
        /// <param name="emailText">Formated text of e-mail</param>
        /// <returns>Result object</returns>
        [OperationContract]
        SharedLibs.DataContracts.Result SendEmail(User user, string emailText);


        /// <summary>
        /// Create invoice
        /// </summary>
        /// <param name="user">Reference to user</param>
        /// <param name="order">Reference to order</param>
        /// <returns>Result object</returns>
        [OperationContract]
        SharedLibs.DataContracts.Result CreateInvoice(User user, Order order);

    }
}
