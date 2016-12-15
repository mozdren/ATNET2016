using System;
using System.ServiceModel;
using SharedLibs.DataContracts;
using SharedLibs.Enums;
using System.IO;

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
        /// Returns an order with requested Guid
        /// </summary>
        /// <param name="guid">Guid of a order</param>
        /// <returns>Requested order object</returns>
        [OperationContract]
        Order GetOrder(Guid guid);


        /// <summary>
        /// Returns the list of orders
        /// </summary>
        /// <returns>List of orders objects</returns>
        [OperationContract]
        Orders GetAllOrders();


        /// <summary>
        /// Create empty order with new Guid Id and basket object, orderNumber, date of order creation and a orderState created.
        /// To this order you can then add particular objects like user, address, etc.
        /// All these items can be then added with help methods (AddUserToOrder, AddAddressToOrder etc.) to newly created order to DB too
        /// Order cannot be created without basket object because of the associaton basket <-> order cardinality is 1 to 0-1 
        /// </summary>
        /// <param name="orderId">Return value of Id of order which was newly created</param>
        /// <param name="basket">Reference to basket object</param>
        /// <returns>Result object</returns>
        [OperationContract]
        Result CreateNewOrder(Basket basket, out Guid orderId);


        /// <summary>
        /// Add user object to created order
        /// </summary>
        /// <param name="orderId">Id of order which you want to add a user object to</param>
        /// <param name="user">Reference to a user object you want to add</param>
        /// <returns>Result object</returns>
        [OperationContract]
        Result AddUserToOrder(Guid orderId, User user);


        /// <summary>
        /// Add address object (in this case delivery address) to created order
        /// </summary>
        /// <param name="orderId">Id of order which you want to add an address object to</param>
        /// <param name="deliveryAddress">Reference to an address object you want to add</param>
        /// <returns>Result object</returns>
        [OperationContract]
        Result AddAddressToOrder(Guid orderId, Address deliveryAddress);


        /// <summary>
        /// Add a billingInformation object to created order
        /// </summary>
        /// <param name="orderId">Id of order which you want to add a billingInformation object to</param>
        /// <param name="billingInformation">Reference to a billingInformation object you want to add</param>
        /// <returns>Result object</returns>
        [OperationContract]
        Result AddBillingInformationToOrder(Guid orderId, BillingInformation billingInformation);


        /// <summary>
        /// Add new order to system
        /// </summary>
        /// <param name="guid">ID of a order</param>
        /// <param name="basket">Reference to a basket object</param>
        /// <param name="user">Reference to a user object</param>
        /// <param name="deliveryAddress">Reference to an address object</param>
        /// <param name="billingInformation">Reference to a billingInformation object</param>
        /// <param name="orderDate">Order creation date</param>
        /// <param name="deliveryDate">Order delivery date</param>
        /// <param name="orderState">Signals current state of order</param>
        /// <returns>Result object</returns>
        [OperationContract]
        Result AddOrder(
            Guid guid, 
            Basket basket,
            User user,
            Address deliveryAddress,
            BillingInformation billingInformation,
            DateTime orderDate,
            DateTime deliveryDate,
            OrderState orderState);


        /// <summary>
        /// Add new order to system
        /// </summary>
        /// <param name="order">Order object</param>
        /// <returns>Result object</returns>
        [OperationContract(Name = "AddOrderByObject")]
        Result AddOrder(Order order);


        /// <summary>
        /// Editation of an order
        /// </summary>
        /// <param name="guid">ID of a order</param>
        /// <param name="basket">Reference to a basket object</param>
        /// <param name="address">Reference to an address object</param>
        /// <param name="billingInformation">Reference to a billingInformation object</param>
        /// <param name="deliveryDate">Order delivery date</param>
        /// <returns>New Order object</returns>
        [OperationContract]
        Order EditOrder(
            Guid guid,
            Basket basket,
            User user,
            Address deliveryAddress,
            BillingInformation billingInformation,
            DateTime deliveryDate);
                


        /// <summary>
        /// Order state change
        /// </summary>
        /// <param name="guid">ID of a order</param>
        /// <param name="state">State of an order</param>
        /// /// <returns>Result object</returns>
        [OperationContract]
        Result ChangeOrderState(Guid guid, OrderStateType newState);


        /// <summary>
        /// Delete an order with requested Guid
        /// </summary>
        /// <param name="guid">Guid of a order</param>
        /// <returns>Result object</returns>
        [OperationContract]
        Result DeleteOrder(Guid guid);


        /// <summary>
        /// Create an e-mail for a client
        /// In case an e-mail is automatic the emailText parameter has to be an empty string
        /// Automatic e-mails are control by the state of order
        /// In case a dealer want to send a handly written e-mail the text will be inserted to the emailText parameter
        /// and to this e-mail will not be inserted an automatic generated text
        /// </summary>
        /// <param name="user">Reference to an user</param>
        /// <param name="order">Reference to an order</param>
        /// <param name="emailText">Formated text of an e-mail</param>
        /// <param name="attachment">Attachment of an e-mail - name of file</param>
        /// <returns>Result object</returns>
        [OperationContract]
        Result CreateEmail(
            User user,
            Order order, 
            string emailText,
            string attachment);


        /// <summary>
        /// Send e-mail to client
        /// </summary>
        /// <param name="user">Reference to an user</param>
        /// <param name="order">Reference to an order</param>
        /// <param name="emailText">Formated text of an e-mail</param>
        /// <param name="attachment">Attachment of an e-mail - name of file</param>
        /// <returns>Result object</returns>
        [OperationContract]
        Result SendEmail(
            User user,
            Order order, 
            string emailText,
            string attachment);


        /// <summary>
        /// Create order overview or invoice or delivery note
        /// Pdf document will contain requested items according to chosen PDF document type
        /// The chosen PDF document type affects the displayed format of document
        /// </summary>
        /// <param name="user">Reference to user</param>
        /// <param name="order">Reference to order</param>
        /// <param name="documentType">Type of PDF document</param>
        /// <param name="pdfFilePath">Return path to pdf file</param>
        /// <returns>Result object</returns>
        [OperationContract]
        Result CreatePDFDocument(
            User user,
            Order order,
            PDFDocumentType documentType, 
            out string pdfFilePath);


        /// <summary>
        /// Returns requested order state according enum order state type
        /// Used enums:
        /// 0 - canceled
        /// 1 - created
        /// 2 - changed
        /// 3 - readyToSend
        /// 4 - sent
        /// 5 - paid
        /// 6 - finished
        /// </summary>
        /// <param name="orderState">Returned Id of requested order state</param>
        /// <param name="orderStateType">Choosen enum value of order state</param>
        /// <returns>Result object</returns>
        [OperationContract]
        Result GetOrderState(OrderStateType orderStateType, out Guid orderState);
    }
}
