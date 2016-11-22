﻿using System;
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
        /// <param name="guid">guid of a order</param>
        /// <returns>requested order</returns>
        [OperationContract]
        Order GetOrder(Guid guid);


        /// <summary>
        /// Returns the list of orders
        /// </summary>
        /// <returns>list of orders</returns>
        [OperationContract]
        Orders GetAllOrders();


        /// <summary>
        /// Add new order to system
        /// </summary>
        /// <param name="guid">ID of a order</param>
        /// <param name="basket">Reference to a basket object</param>
        /// <param name="address">Reference to an address object</param>
        /// <param name="billingInformation">Reference to a billingInformation object</param>
        /// <param name="orderDate">Order creation date</param>
        /// <param name="deliveryDate">Order delivery date</param>
        /// <returns>Result object</returns>
        [OperationContract]
        Result AddOrder(
            Guid guid, 
            SharedLibs.DataContracts.Basket basket,
            SharedLibs.DataContracts.Address deliveryAddress,
            SharedLibs.DataContracts.BillingInformation billingInformation,
            DateTime orderDate, DateTime deliveryDate, OrderStateType orderState);


        /// <summary>
        /// Add new order to system
        /// </summary>
        /// <param name="order">Order object</param>
        /// <returns>Result object</returns>
        [OperationContract(Name = "AddOrderByObject")]
        Result AddOrder(SharedLibs.DataContracts.Order order);


        /// <summary>
        /// Editation of an order
        /// </summary>
        /// <param name="guid">ID of a order</param>
        /// <param name="basket">Reference to a basket object</param>
        /// <param name="address">Reference to an address object</param>
        /// <param name="billingInformation">Reference to a billingInformation object</param>
        /// <param name="deliveryDate">Order delivery date</param>
        /// <param name="orderState">Signals current state of order</param>
        /// <returns>New Order object</returns>
        [OperationContract]
        Order EditOrder(
            Guid guid,
            SharedLibs.DataContracts.Basket basket,
            SharedLibs.DataContracts.Address deliveryAddress,
            SharedLibs.DataContracts.BillingInformation billingInformation,
            DateTime deliveryDate, OrderStateType orderState);
                


        /// <summary>
        /// Order state change
        /// </summary>
        /// <param name="guid">ID of a order</param>
        /// <param name="state">State of an order</param>
        /// /// <returns>Result object</returns>
        [OperationContract]
        Result ChangeOrderState(Guid guid, SharedLibs.Enums.OrderStateType orderState);


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
            string emailText, string attachment);


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
            string emailText, string attachment);


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
    }
}
