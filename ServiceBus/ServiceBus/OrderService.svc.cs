using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SharedLibs.DataContracts;
using SharedLibs.Enums;
using SharedLibs;
using System.Net.Mail;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ServiceBus
{
    public class OrderService : IOrderService
    {

        // Implementation and other parts - I will make an effort to do it per partes :-) KZ
        // At this time is not created table order and that is part of code hidden

        /// <summary>
        /// Method returns order according requested ID number
        /// </summary>
        /// <param name="guid">Order ID number</param>
        /// <returns>Order object</returns>

        public Order GetOrder(Guid guid)
        {
            try
            {
                using (var context = new EntityModels.ServiceBusDatabaseEntities())
                {
                    var order = context.Orders.FirstOrDefault(o => o.Id == guid);

                    if (order == null)
                    {
                        return new SharedLibs.DataContracts.Order
                        {
                            Result = SharedLibs.DataContracts.Result.ErrorFormat("Requested order ID number {0} was not found.", guid)
                        };
                    }

                    // tests nullable properties
                    bool basketValue = true;
                    bool addressValue = true;
                    bool billingInformationValue = true;
                    bool billingAddressValue = true;
                    bool basketItemValue = true;
                    bool campaignItemValue = true;

                    var orderStatus = context.OrderStatus.FirstOrDefault(os => os.Id == order.OrderStatus.Id);                    
                    var basket = context.Baskets.FirstOrDefault(b => b.Id == order.Basket.Id);

                    if (basket == null)
                    {
                        basketValue = false;
                    }

                    var deliveryAddress = context.Addresses.FirstOrDefault(da => da.Id == order.Address.Id);

                    if (deliveryAddress == null)
                    {
                        addressValue = false;
                    }

                    var billingInformation = context.BillingInformations.FirstOrDefault(bi => bi.Id == order.BillingInformation.Id);

                    if (billingInformation == null)
                    {
                        billingInformationValue = false;
                    }

                    var billingAddress = context.Addresses.FirstOrDefault(ba => ba.Id == billingInformation.Address.Id);

                    if (billingAddress == null)
                    {
                        billingAddressValue = false;
                    }

                    var basketItemsList = context.BasketItems.Where(bil => bil.BasketId == basket.Id);

                    if (basketItemsList == null)
                    {
                        basketItemValue = false;
                    }

                    var campaignItem = context.CampaignItems.Where(ci => ci.BasketId == basket.Id);
                    var campaignItemsList = context.Campaigns.Where(cil => campaignItem.Any(ci => ci.CampaignId == cil.Id));

                    if (campaignItem == null || campaignItemsList == null)
                    {
                        campaignItemValue = false;
                    }

                    List<BasketItem> basketItemList = new List<BasketItem>();
                    foreach (var bait in basketItemsList)
                    {
                        var product = context.Products.FirstOrDefault(p => p.Id == bait.ProductId);
                        Product productItem = new Product() { ID = product.Id, Name = product.Name, Price = product.Price.HasValue ? product.Price.Value : 0.0 };
                        basketItemList.Add(new BasketItem() { Id = bait.Id, Product = productItem, Quantity = bait.Quantity, Active = true }); // Table basketItem does not contain property Active
                    }
                    List<Campaign> campaignItemList = new List<Campaign>();
                    foreach (var cait in campaignItemsList)
                    {
                        campaignItemList.Add(new Campaign() { Id = cait.Id, Name = cait.Name, Discount = cait.Discount.HasValue ? cait.Discount.Value : 0.0 });
                    }

                    return new SharedLibs.DataContracts.Order
                    {
                        Id = guid,
                        Basket = (basketValue == true) ? new Basket()
                        {
                            Id = basket.Id,
                            BasketItems = (basketItemValue == true) ? basketItemList : null,
                            BasketCampaings = (campaignItemValue == true) ? campaignItemList : null
                        } : null,
                        DeliveryAddress = (addressValue == true) ? new Address()
                        {
                            Id = deliveryAddress.Id,
                            PostCode = deliveryAddress.PostCode,
                            HouseNumber = deliveryAddress.HouseNumber,
                            HouseNumberExtension = deliveryAddress.HouseNumberExtension,
                            Street = deliveryAddress.Street,
                            City = deliveryAddress.City,
                            District = deliveryAddress.District,
                            DoorNumber = deliveryAddress.DoorNumber
                        } : null,
                        BillingInformation = (billingInformationValue == true) ? new BillingInformation()
                        {
                            Id = billingInformation.Id,
                            BillingAddress = (billingAddressValue == true) ? new Address()
                            {
                                Id = billingAddress.Id,
                                PostCode = billingAddress.PostCode,
                                HouseNumber = billingAddress.HouseNumber,
                                HouseNumberExtension = billingAddress.HouseNumberExtension,
                                Street = billingAddress.Street,
                                City = billingAddress.City,
                                District = billingAddress.District,
                                DoorNumber = billingAddress.DoorNumber
                            } : null,
                            BIC = billingInformation.BIC,
                            IBAN = billingInformation.IBAN
                        } : null,
                        OrderDate = (DateTime)(order.OrderDate.HasValue ? order.OrderDate.Value : Convert.ToDateTime("1.1.2010")),
                        DeliveryDate = (DateTime)(order.DeliveryDate.HasValue ? order.DeliveryDate.Value : Convert.ToDateTime("1.1.2010")),
                        OrderState = (OrderStateType)orderStatus.Status,
                        Result = SharedLibs.DataContracts.Result.SuccessFormat("Requested order ID number {0} was found.", guid)

                    };

                }
            }
            catch (Exception exception)
            {

                return new SharedLibs.DataContracts.Order
                {
                    Result = SharedLibs.DataContracts.Result.FatalFormat("In method OrderService.GetOrder was thrown exception: {0}", exception.Message)
                };

            }
        }


        /// <summary>
        /// Method returns list of orders in collection
        /// </summary>
        /// <returns>List of orders</returns>
        public Orders GetAllOrders()
        {
            /*try
            {
                using (var context = new ServiceBusDatabaseEntities())
                {
                    var orderList = new List<Order>();

                    foreach (var order in context.Orders)
                    {
                        orderList.Add(new SharedLibs.DataContracts.Order
                        {
                            Id = order.Id,
                            Basket = order.Basket,
                            DeliveryAddress = order.DeliveryAddress,
                            BillingInformation = order.BillingInformation,
                            OrderDate = order.OrderDate,
                            DeliveryDate = order.DeliveryDate,
                            OrderState = order.OrderState
                        });
                    }

                    if (orderList.Count == 0)
                    {
                        return new SharedLibs.DataContracts.Orders
                        {
                            Result = SharedLibs.DataContracts.Result.WarningFormat("It was not found any item of orders."),
                            Items = orderList
                        };
                    }
                    else
                    {
                        return new SharedLibs.DataContracts.Orders
                        {
                            Result = SharedLibs.DataContracts.Result.Success(),
                            Items = orderList
                        };
                    }
                    
                }
            }
            catch (Exception exception)
            {
                return new SharedLibs.DataContracts.Orders
                {
                    Result = SharedLibs.DataContracts.Result.FatalFormat("In method OrderService.GetAllOrders was thrown exception: {0}",
                                                                         exception.Message)
                };
            }*/

//********* delete this code after finishing implementation method GetOrder ************

            return new SharedLibs.DataContracts.Orders
            {
                Result = Result.Fatal("Not finish")
            };
        }


        /// <summary>
        /// Method adds new order to system
        /// <param name="guid">ID of a order</param>
        /// <param name="basket">Reference to basket object</param>
        /// <param name="address">Reference to address object</param>
        /// <param name="billingInformation">Reference to billingInformation object</param>
        /// <param name="orderDate">Order creation date</param>
        /// <param name="deliveryDate">Order delivery date</param>
        /// </summary>
        /// <returns>Result object</returns>
        public Result AddOrder(
            Guid guid,
            Basket basket,
            Address deliveryAddress,
            BillingInformation billingInformation,
            DateTime orderDate, DateTime deliveryDate, OrderStateType orderState)
        {
            /*try
            {
                using (var context = new ServiceBusDatabaseEntities())
                {
                    if (basket != null && deliveryAddress != null && billingInformation != null &&
                        orderDate == DateTime.Now && deliveryDate >= DateTime.Now)
                    {
                        var newOrder = new SharedLibs.DataContracts.Order
                        {
                            Id = guid,
                            Basket = basket,
                            DeliveryAddress = deliveryAddress,
                            BillingInformation = billingInformation,
                            OrderDate = orderDate,
                            DeliveryDate = deliveryDate,
                            OrderState = orderState
                        };

                        context.Orders.Add(newOrder);
                        context.SaveChanges();

                        return SharedLibs.DataContracts.Result.SuccessFormat("New order ID number {0} was stored successfully.", guid);
                    }
                    else
                    {
                        return SharedLibs.DataContracts.Result.ErrorFormat("Input parameters have not correct value.");
                    }
                }
            }
            catch (Exception exception)
            {
                return SharedLibs.DataContracts.Result.FatalFormat("In method OrderService.AddOrder was thrown exception: {0}",
                                                                         exception.Message);
            } */
            return Result.Fatal("Not finished");
        }


        /// <summary>
        /// Method adds new order to system using order object
        /// <param name="order">Order object</param>
        /// </summary>
        /// <returns>Result object</returns>
        public Result AddOrder(Order order)
        {
            return AddOrder(order.Id, order.Basket, order.DeliveryAddress, order.BillingInformation,
                            order.OrderDate, order.DeliveryDate, order.OrderState);
        }


        /// <summary>
        /// Method allows editing items of order
        /// </summary>
        /// <param name="guid">ID of a order</param>
        /// <param name="basket">Reference to basket object</param>
        /// <param name="address">Reference to address object</param>
        /// <param name="billingInformation">Reference to billingInformation object</param>
        /// <param name="deliveryDate">Order delivery date</param>
        /// <param name="orderState">Signals current state of order</param>
        /// <returns>New Order object</returns>
        public Order EditOrder(
            Guid guid, Basket basket,
            Address deliveryAddress,
            BillingInformation billingInformation,
            DateTime deliveryDate, OrderStateType orderState)
        {
            /*try
            {
                var storedOrder = this.GetOrder(guid);

                if (storedOrder.Result.ResultType == SharedLibs.Enums.ResultType.Success)
                {
                    var alteredOrder = new SharedLibs.DataContracts.Order
                    {
                        Id = storedOrder.Id,
                        Basket = storedOrder.Basket,
                        DeliveryAddress = storedOrder.DeliveryAddress,
                        BillingInformation = storedOrder.BillingInformation,
                        OrderDate = storedOrder.OrderDate,
                        DeliveryDate = storedOrder.DeliveryDate,
                        OrderState = storedOrder.OrderState
                    };


                    using (var context = new ServiceBusDatabaseEntities())
                    {
                        context.Orders.Attach(alteredOrder);

                        // Test data for change of basket object
                        if (basket != null && basket != storedOrder.Basket)
                        {
                            alteredOrder.Basket = basket;
                        }

                        // Test data for change of address object
                        if (deliveryAddress != null && deliveryAddress != storedOrder.DeliveryAddress)
                        {
                            alteredOrder.DeliveryAddress = deliveryAddress;
                        }

                        // Test data for change of billing information object
                        if (billingInformation != null && billingInformation != storedOrder.BillingInformation)
                        {
                            alteredOrder.BillingInformation = billingInformation;
                        }

                        // Test data for change of delivery date
                        if (deliveryDate.Year != 0 && deliveryDate.Month != 0 && deliveryDate.Day != 0 && deliveryDate < DateTime.Now)
                        {
                            alteredOrder.DeliveryDate = deliveryDate;
                        }

                        // If no change was performed it is returned origin order object
                        if (context.Entry(alteredOrder).State == System.Data.Entity.EntityState.Unchanged)
                        {
                            storedOrder.Result = SharedLibs.DataContracts.Result.WarningFormat("Order ID number {0} was not changed.", guid);
                            return storedOrder;
                        }
                        else
                        {
                            alteredOrder.OrderState = SharedLibs.Enums.OrderStateType.Changed;
                            context.SaveChanges();

                            return new SharedLibs.DataContracts.Order
                            {
                                Id = alteredOrder.Id,
                                Basket = alteredOrder.Basket,
                                DeliveryAddress = alteredOrder.DeliveryAddress,
                                BillingInformation = alteredOrder.BillingInformation,
                                OrderDate = alteredOrder.OrderDate,
                                DeliveryDate = alteredOrder.DeliveryDate,
                                OrderState = alteredOrder.OrderState
                            };
                        }
                    }
                }
                else
                {
                    return storedOrder;
                }
            }
            catch (Exception exception)
            {
                return new SharedLibs.DataContracts.Order
                {
                    Result = SharedLibs.DataContracts.Result.FatalFormat("In method OrderService.EditOrder was thrown exception: {0}.",
                                                                         exception.Message)
                };
            }*/

            return new Order
            {
                Result = Result.Fatal("Not finished")
            };
        }


        /// <summary>
        /// Order state change
        /// </summary>
        /// <param name="guid">ID of a order</param>
        /// <param name="state">State of order</param>
        /// /// <returns>Result object</returns>
        public Result ChangeOrderState(
            Guid guid, OrderStateType orderState)
        {
            /*try
            {
                var storedOrder = this.GetOrder(guid);

                if (storedOrder.Result.ResultType == SharedLibs.Enums.ResultType.Success)
                {
                    var alteredOrder = new SharedLibs.DataContracts.Order
                    {
                        Id = storedOrder.Id,
                        Basket = storedOrder.Basket,
                        DeliveryAddress = storedOrder.DeliveryAddress,
                        BillingInformation = storedOrder.BillingInformation,
                        OrderDate = storedOrder.OrderDate,
                        DeliveryDate = storedOrder.DeliveryDate,
                        OrderState = storedOrder.OrderState
                    };

                    using (var context = new ServiceBusDatabaseEntities())
                    {
                       
                        context.Orders.Attach(alteredOrder);

                        alteredOrder.OrderState = orderState;

                        if (context.Entry(alteredOrder).State == System.Data.Entity.EntityState.Unchanged)
                        {
                            return SharedLibs.DataContracts.Result.WarningFormat("State of order ID number {0} was not changed.", guid);
                        }
                        else
                        {
                            context.SaveChanges();                            
                            return SharedLibs.DataContracts.Result.SuccessFormat("State of order ID number {0} was changed");
                        }                        
                    }
                }
                else
                {
                    return SharedLibs.DataContracts.Result.WarningFormat("Result of requested order ID number {0} is not success." +
                                                                          " Order state cannot be changed.", guid);
                }
            }
            catch (Exception exception)
            {
                return SharedLibs.DataContracts.Result.FatalFormat("In method OrderService.ChangeOrderState was thrown exception: {0}",
                                                                        exception.Message);           
            }*/
            return Result.Fatal("Not finished");
        }


        /// <summary>
        /// Delete order with specific Guid
        /// </summary>
        /// <param name="guid">guid of a order</param>
        /// <returns>Result object</returns>
        public Result DeleteOrder(Guid guid)
        {
            /*try
            {
                using (var context = new ServiceBusDatabaseEntities())
                {
                    var storedOrder = this.GetOrder(guid);

                    if (storedOrder == null)
                    {
                        return SharedLibs.DataContracts.Result.ErrorFormat("Requested order ID number {0} was not found.", guid);
                    }

                    context.Orders.Remove(storedOrder);
                    context.SaveChanges();

                    return SharedLibs.DataContracts.Result.SuccessFormat("Requested order ID number {0} was deleted successfully.", guid);
                }
            }
            catch (Exception exception)
            {
                return SharedLibs.DataContracts.Result.FatalFormat("In method OrderService.DeleteOrder was thrown exception: {0}.",
                                                                         exception.Message);
            }*/
            return Result.Fatal("Not finished");
        }


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
        public Result CreateEmail(
            User user,
            Order order, 
            string emailText, string attachment)
        {
            try
            {
                if (user != null && order != null)
                {
                    // A case the e-mail is written handly
                    if (!string.IsNullOrEmpty(emailText))
                    {
                        SendEmail(user, order, emailText, attachment);
                    }
                    // An automatic e-mail
                    else
                    {
                        string variableText = "";

                        switch (order.OrderState)
                        {
                            case OrderStateType.Canceled:
                                {
                                    variableText = " byla zrušena.";
                                    break;
                                }

                            case OrderStateType.Created:
                                {
                                    variableText = " byla přijata.";
                                    break;
                                }

                            case OrderStateType.Changed:
                                {
                                    variableText = " byla změněna.";
                                    break;
                                }

                            case OrderStateType.ReadyToSend:
                                {
                                    variableText = " je připravena k odeslání.";
                                    break;
                                }

                            case OrderStateType.Sent:
                                {
                                    variableText = " byla odeslána.";
                                    break;
                                }

                            default:
                                {
                                    return Result.ErrorFormat("OrderStateType in order ID number {0} is incorrect", order.Id);
                                }
                        }

                        string text = "Vážený zákazníku\n\n" +
                                      "Vaše objednávka číslo " + order.Id + variableText + ".\n\n" +
                                      "Informace o Vaší objednávce jsou v příloze e-mailu.\n\n" +
                                      "Děkujeme, že jste využili služeb našeho internetového obchodu.\n" +
                                      "V případě problému nás prosím kontaktujte na tel.: 123456789, \n" +
                                      "nebo na e-mailu our_company@vsb.cz.\n\n\n\n" +
                                      "Tým our_company_vsb";

                        this.SendEmail(user, order, text, attachment);
                    }

                    return Result.Success("E-mail was created correctly.");
                }
                else
                {
                    throw new Exception("User or order parameter is a null reference.");
                }

            }
            catch (Exception exception)
            {
                return Result.FatalFormat("In method OrderService.CreateEmail was thrown exception: {0}.", exception.Message);
            }
        }


        /// <summary>
        /// Send e-mail to client
        /// </summary>
        /// <param name="user">Reference to user</param>
        /// <param name="order">Reference to an order</param>
        /// <param name="emailText">Formated text of e-mail</param>
        /// <param name="attachment">Attachment of an e-mail - name of file</param>
        /// <returns>Result object</returns>
        public Result SendEmail(
            User user,
            Order order,
            string emailText, string attachment)
        {
            
            try
            {
                if (user != null && order != null && !String.IsNullOrEmpty(emailText))
                {
                    string from = "our_company@vsb.cz";   // Temporary sender
                    string subject = "Objednávka číslo " + order.Id;
                    string smtpServer = "smtpServer";   // Temporary server name

                    MailMessage message = new MailMessage(from, user.EmailAddress, subject, emailText);
                    SmtpClient client = new SmtpClient(smtpServer); 

                    if (message != null && client != null)
                    {
                        bool attachmentFailure = false;

                        if (!string.IsNullOrEmpty(attachment))
                        {
                            try
                            {
                                message.Attachments.Add(new Attachment(attachment));
                            }
                            catch (Exception exception)
                            {
                                attachmentFailure = true;
                                throw new Exception(exception.Message);
                            }
                        }

                        if (!attachmentFailure)
                        {
                            client.Send(message);
                            return Result.SuccessFormat("E-mail was correctly sent to {0}.", user.EmailAddress);
                        }
                        else
                        {
                            return Result.Error("E-mail was not sent. Attachment failure.");
                        }
                    }
                    else
                    {
                        throw new Exception("Memory allocation failure of MailMessage or SmtpClient.");
                    }                    
                }
                else
                {
                    return Result.ErrorFormat("Input parameters to create e-mail to {0} have not correct value.", user.EmailAddress);                                                                      
                }
            }
            catch (Exception exception)
            {
                return Result.FatalFormat("In method OrderService.SendEmail was thrown exception: {0}.", exception.Message);
            }
        }


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
        public Result CreatePDFDocument(
            User user,
            Order order,
            PDFDocumentType documentType,
            out string pdfFilePath)
        {
            
            int itemsCount = order.Basket.BasketItems.Count;            
            string orderNumber = "Přehled objednávky číslo:  " + order.Id;
            string pageNumberMark = string.Empty;
            bool setFullItemsPerPageCount = false;

            string userAddressStreet = user.UserAddress.Street + " " + user.UserAddress.HouseNumber +
                (string.IsNullOrEmpty(user.UserAddress.HouseNumberExtension) ? string.Empty : "/" + user.UserAddress.HouseNumberExtension) +
                (string.IsNullOrEmpty(user.UserAddress.DoorNumber) ? string.Empty : " dveře č. " + user.UserAddress.DoorNumber);

            string deliveryAddressStreet = order.BillingInformation.BillingAddress.Street + " " + order.BillingInformation.BillingAddress.HouseNumber +
                (string.IsNullOrEmpty(order.BillingInformation.BillingAddress.HouseNumberExtension) ? string.Empty :
                "/" + order.BillingInformation.BillingAddress.HouseNumberExtension) +
                (string.IsNullOrEmpty(order.BillingInformation.BillingAddress.DoorNumber) ? string.Empty :
                " dveře č. " + order.BillingInformation.BillingAddress.DoorNumber);

            string userAddressTown = user.UserAddress.City + " PSČ: " + user.UserAddress.PostCode +
                (string.IsNullOrEmpty(user.UserAddress.District) ? string.Empty : " okres " + user.UserAddress.District);

            string deliveryAddressTown = order.BillingInformation.BillingAddress.City + " PSČ: " + order.BillingInformation.BillingAddress.PostCode +
                (string.IsNullOrEmpty(order.BillingInformation.BillingAddress.District) ? string.Empty :
                " okres " + order.BillingInformation.BillingAddress.District);


          
            if (documentType == PDFDocumentType.Order)
            {
                PdfDocumentFields.itemsPerPageCount = PdfDocumentFields.ORDER_ITEMS_AT_FIRST_PAGE_COUNT;
                PdfDocumentFields.generalItemsPerPageCount = PdfDocumentFields.ORDER_ITEMS_AT_OTHER_PAGE_COUNT;
            }

            if (documentType == PDFDocumentType.Invoice)
            {
                PdfDocumentFields.itemsPerPageCount = PdfDocumentFields.INVOICE_ITEMS_AT_FIRST_PAGE_COUNT;
                PdfDocumentFields.generalItemsPerPageCount = PdfDocumentFields.INVOICE_ITEMS_AT_OTHER_PAGE_COUNT;
            }

            if (documentType == PDFDocumentType.DeliveryNote)
            {
                PdfDocumentFields.itemsPerPageCount = PdfDocumentFields.DELIVERY_NOTE_ITEMS_AT_FIRST_PAGE_COUNT;
                PdfDocumentFields.generalItemsPerPageCount = PdfDocumentFields.DELIVERY_NOTE_ITEMS_AT_OTHER_PAGE_COUNT;
            }

            int pagesCount = (itemsCount <= PdfDocumentFields.itemsPerPageCount) ? 1 :
                ((itemsCount - PdfDocumentFields.itemsPerPageCount - 1) / PdfDocumentFields.generalItemsPerPageCount) + 2;

            Document doc = null;

            try
            {
                pdfFilePath = string.Empty;

                if (!Directory.Exists(PdfDocumentFields.DIRECTORY_PATH))
                {
                    Directory.CreateDirectory(PdfDocumentFields.DIRECTORY_PATH);
                }

                FileStream fs = new FileStream(PdfDocumentFields.FILE_PATH, FileMode.Create);

                if (documentType == PDFDocumentType.DeliveryNote)
                {
                    doc = new Document(PageSize.A4.Rotate());
                }
                else
                {
                    doc = new Document(PageSize.A4);
                }

                PdfWriter pdfw = PdfWriter.GetInstance(doc, fs);
                doc.Open();

                PdfPTable itemsTable = null;
                Paragraph paragraph = null;
                PdfPCell numberCell = null;
                PdfPCell cell1 = null;
                PdfPCell cell2 = null;
                PdfPCell cell3 = null;
                PdfPCell cell4 = null;
                PdfPCell cell5 = null;

                foreach (BasketItem item in order.Basket.BasketItems)
                {
                    if (PdfDocumentFields.itemNumber == 0)
                    {
                        // page mark
                        pageNumberMark = "Strana " + PdfDocumentFields.pageNumber.ToString() + " z " + pagesCount.ToString();
                        paragraph = new Paragraph(new Chunk(pageNumberMark, PdfDocumentFields.smallNormalFont));
                        paragraph.Alignment = Element.ALIGN_RIGHT;
                        doc.Add(paragraph);

                        if (PdfDocumentFields.itemNumber == 0 && PdfDocumentFields.pageNumber == 1)
                        {
                            if (documentType == PDFDocumentType.Order)
                            {
                                paragraph = new Paragraph(new Chunk(PdfDocumentFields.COMPANY_TITLE, PdfDocumentFields.bigBoldFont));
                                paragraph.Add(new Chunk(PdfDocumentFields.horizontalSpace));
                                paragraph.Add(new Chunk(orderNumber, PdfDocumentFields.middleNormalFont));
                                doc.Add(paragraph);
                            }

                            if (documentType == PDFDocumentType.Invoice)
                            {
                                doc.Add(new Chunk(Chunk.NEWLINE));
                                paragraph = new Paragraph(new Chunk("Faktura", PdfDocumentFields.middleBoldFont));
                                paragraph.Alignment = Element.ALIGN_CENTER;
                                doc.Add(paragraph);
                                doc.Add(new Chunk(Chunk.NEWLINE));
                                doc.Add(new Paragraph(new Chunk("Dodavatel:", PdfDocumentFields.middleBoldFont)));
                                doc.Add(new Paragraph(new Chunk(PdfDocumentFields.COMPANY_TITLE + PdfDocumentFields.COMPANY_ADDRESS, PdfDocumentFields.smallNormalFont)));
                                doc.Add(new Paragraph(new Chunk(PdfDocumentFields.COMPANY_IDENT_NUMBERS, PdfDocumentFields.smallNormalFont)));
                                doc.Add(new Paragraph(new Chunk(PdfDocumentFields.REGISTERED_WIDTH_INSTITUTION_PART_I, PdfDocumentFields.smallNormalFont)));
                                doc.Add(new Paragraph(new Chunk(PdfDocumentFields.REGISTERED_WIDTH_INSTITUTION_PART_II, PdfDocumentFields.smallNormalFont)));
                                doc.Add(new Chunk(Chunk.NEWLINE));

                                PdfPTable customerTable = new PdfPTable(PdfDocumentFields.INVOICE_CUSTOMER_COLUMN_COUNT);
                                customerTable.WidthPercentage = 100.0f;
                                cell1 = new PdfPCell(new Phrase("Zákazník:", PdfDocumentFields.middleBoldFont));
                                cell1.Border = Rectangle.NO_BORDER;
                                cell2 = new PdfPCell(new Phrase("Dodací adresa:", PdfDocumentFields.middleBoldFont));
                                cell2.Border = Rectangle.NO_BORDER;
                                customerTable.AddCell(cell1);
                                customerTable.AddCell(cell2);
                                cell1 = new PdfPCell(new Phrase(user.Name + " " + user.Surname, PdfDocumentFields.smallNormalFont));
                                cell1.Border = Rectangle.NO_BORDER;
                                cell2 = new PdfPCell(new Phrase(user.Name + " " + user.Surname, PdfDocumentFields.smallNormalFont));
                                cell2.Border = Rectangle.NO_BORDER;
                                customerTable.AddCell(cell1);
                                customerTable.AddCell(cell2);
                                cell1 = new PdfPCell(new Phrase(userAddressStreet, PdfDocumentFields.smallNormalFont));
                                cell1.Border = Rectangle.NO_BORDER;
                                cell2 = new PdfPCell(new Phrase(deliveryAddressStreet, PdfDocumentFields.smallNormalFont));
                                cell2.Border = Rectangle.NO_BORDER;
                                customerTable.AddCell(cell1);
                                customerTable.AddCell(cell2);
                                cell1 = new PdfPCell(new Phrase(userAddressTown, PdfDocumentFields.smallNormalFont));
                                cell1.Border = Rectangle.NO_BORDER;
                                cell2 = new PdfPCell(new Phrase(deliveryAddressTown, PdfDocumentFields.smallNormalFont));
                                cell2.Border = Rectangle.NO_BORDER;
                                customerTable.AddCell(cell1);
                                customerTable.AddCell(cell2);
                                doc.Add(customerTable);
                                doc.Add(new Paragraph(new Chunk(PdfDocumentFields.thinLineSeparator)));

                                PdfPTable infoTable = new PdfPTable(PdfDocumentFields.INVOICE_INFO_COLUMN_COUNT);
                                infoTable.WidthPercentage = 100.0f;
                                infoTable.SpacingBefore = 10.0f;
                                cell1 = new PdfPCell(new Phrase("Číslo faktury:", PdfDocumentFields.smallNormalFont));
                                cell1.Border = Rectangle.NO_BORDER;
                                cell2 = new PdfPCell(new Phrase(order.Id.ToString(), PdfDocumentFields.smallNormalFont));
                                cell2.Border = Rectangle.NO_BORDER;
                                infoTable.AddCell(cell1);
                                infoTable.AddCell(cell2);
                                cell1 = new PdfPCell(new Phrase("Datum vystavení faktury:", PdfDocumentFields.smallNormalFont));
                                cell1.Border = Rectangle.NO_BORDER;
                                cell2 = new PdfPCell(new Phrase(order.DeliveryDate.ToString(), PdfDocumentFields.smallNormalFont));
                                cell2.Border = Rectangle.NO_BORDER;
                                infoTable.AddCell(cell1);
                                infoTable.AddCell(cell2);
                                cell1 = new PdfPCell(new Phrase("Datum zdanitelného plnění:", PdfDocumentFields.smallNormalFont));
                                cell1.Border = Rectangle.NO_BORDER;
                                cell2 = new PdfPCell(new Phrase(order.DeliveryDate.ToString(), PdfDocumentFields.smallNormalFont));
                                cell2.Border = Rectangle.NO_BORDER;
                                infoTable.AddCell(cell1);
                                infoTable.AddCell(cell2);
                                cell1 = new PdfPCell(new Phrase("Číslo objednávky:", PdfDocumentFields.smallNormalFont));
                                cell1.Border = Rectangle.NO_BORDER;
                                cell2 = new PdfPCell(new Phrase(order.Id.ToString(), PdfDocumentFields.smallNormalFont));
                                cell2.Border = Rectangle.NO_BORDER;
                                infoTable.AddCell(cell1);
                                infoTable.AddCell(cell2);
                                cell1 = new PdfPCell(new Phrase("Datum objednávky:", PdfDocumentFields.smallNormalFont));
                                cell1.Border = Rectangle.NO_BORDER;
                                cell2 = new PdfPCell(new Phrase(order.OrderDate.ToString(), PdfDocumentFields.smallNormalFont));
                                cell2.Border = Rectangle.NO_BORDER;
                                infoTable.AddCell(cell1);
                                infoTable.AddCell(cell2);
                                doc.Add(infoTable);
                                doc.Add(new Paragraph(new Chunk(PdfDocumentFields.lineSeparator)));
                            }

                            if (documentType == PDFDocumentType.DeliveryNote)
                            {
                                paragraph = new Paragraph(new Chunk("Dodací list", PdfDocumentFields.middleBoldFont));
                                paragraph.Alignment = Element.ALIGN_CENTER;
                                doc.Add(paragraph);
                                doc.Add(new Chunk(Chunk.NEWLINE));

                                PdfPTable infoTable = new PdfPTable(PdfDocumentFields.DELIVERY_NOTE_INFO_COLUMN_COUNT);
                                infoTable.WidthPercentage = 100.0f;
                                infoTable.SetWidths(PdfDocumentFields.deliveryNoteInfoTableColumnWidths);

                                PdfPTable nestedTable = new PdfPTable(1);
                                cell1 = new PdfPCell(new Phrase("Dodavatel: " + PdfDocumentFields.COMPANY_TITLE, PdfDocumentFields.smallNormalFont));
                                cell1.Border = Rectangle.NO_BORDER;
                                cell2 = new PdfPCell(new Phrase("Zásilkový prodej B2C nebo B2B", PdfDocumentFields.smallNormalFont));
                                cell2.Border = Rectangle.NO_BORDER;
                                cell3 = new PdfPCell(new Phrase(PdfDocumentFields.COMPANY_ADDRESS, PdfDocumentFields.smallNormalFont));
                                cell3.Border = Rectangle.NO_BORDER;
                                cell4 = new PdfPCell(new Phrase(PdfDocumentFields.COMPANY_IDENT_NUMBERS, PdfDocumentFields.smallNormalFont));
                                cell4.Border = Rectangle.NO_BORDER;
                                nestedTable.AddCell(cell1);
                                nestedTable.AddCell(cell2);
                                nestedTable.AddCell(cell3);
                                nestedTable.AddCell(cell4);
                                infoTable.AddCell(new PdfPCell(nestedTable));

                                nestedTable = new PdfPTable(1);
                                cell1 = new PdfPCell(new Phrase("Odběratel:", PdfDocumentFields.smallNormalFont));
                                cell1.Border = Rectangle.NO_BORDER;
                                cell2 = new PdfPCell(new Phrase(user.Name + " " + user.Surname, PdfDocumentFields.smallNormalFont));
                                cell2.Border = Rectangle.NO_BORDER;
                                cell3 = new PdfPCell(new Phrase(userAddressStreet + " " + userAddressTown, PdfDocumentFields.smallNormalFont));
                                cell3.Border = Rectangle.NO_BORDER;
                                cell4 = new PdfPCell(new Phrase(user.EmailAddress, PdfDocumentFields.smallNormalFont));
                                cell4.Border = Rectangle.NO_BORDER;
                                nestedTable.AddCell(cell1);
                                nestedTable.AddCell(cell2);
                                nestedTable.AddCell(cell3);
                                nestedTable.AddCell(cell4);
                                infoTable.AddCell(new PdfPCell(nestedTable));

                                nestedTable = new PdfPTable(1);
                                cell1 = new PdfPCell(new Phrase("Datum vystavení:", PdfDocumentFields.smallNormalFont));
                                cell1.Border = Rectangle.NO_BORDER;
                                cell2 = new PdfPCell(new Phrase(order.DeliveryDate.ToString(), PdfDocumentFields.smallNormalFont));
                                cell2.Border = Rectangle.NO_BORDER;
                                cell3 = new PdfPCell(new Phrase("Kód zásilky:", PdfDocumentFields.smallNormalFont));
                                cell3.Border = Rectangle.NO_BORDER;
                                cell4 = new PdfPCell(new Phrase(order.Id.ToString(), PdfDocumentFields.smallNormalFont));
                                cell4.Border = Rectangle.NO_BORDER;
                                nestedTable.AddCell(cell1);
                                nestedTable.AddCell(cell2);
                                nestedTable.AddCell(cell3);
                                nestedTable.AddCell(cell4);
                                infoTable.AddCell(new PdfPCell(nestedTable));
                                doc.Add(infoTable);
                            }
                        }

                        if (documentType == PDFDocumentType.Order)
                        {
                            doc.Add(new Paragraph(new Chunk(PdfDocumentFields.lineSeparator)));
                            doc.Add(new Paragraph(" "));

                            PdfPTable headerTable = new PdfPTable(PdfDocumentFields.ORDER_COLUMN_COUNT);
                            headerTable.WidthPercentage = 100;
                            headerTable.SetWidths(PdfDocumentFields.orderColumnWidths);

                            cell1 = new PdfPCell(new Phrase("Název", PdfDocumentFields.smallNormalFont));
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell1.BorderColor = BaseColor.WHITE;
                            cell2 = new PdfPCell(new Phrase("Cena s DPH / Ks", PdfDocumentFields.smallNormalFont));
                            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell2.BorderColor = BaseColor.WHITE;
                            cell3 = new PdfPCell(new Phrase("Množství", PdfDocumentFields.smallNormalFont));
                            cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell3.BorderColor = BaseColor.WHITE;
                            cell4 = new PdfPCell(new Phrase("Celkem s DPH", PdfDocumentFields.smallNormalFont));
                            cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell4.BorderColor = BaseColor.WHITE;

                            headerTable.AddCell(cell1);
                            headerTable.AddCell(cell2);
                            headerTable.AddCell(cell3);
                            headerTable.AddCell(cell4);
                            doc.Add(headerTable);

                            paragraph = new Paragraph(new Chunk(PdfDocumentFields.thinLineSeparator));
                            doc.Add(paragraph);

                            itemsTable = new PdfPTable(PdfDocumentFields.ORDER_COLUMN_COUNT);
                            itemsTable.SpacingBefore = 10.0f;
                            itemsTable.WidthPercentage = 100.0f;
                            itemsTable.SetWidths(PdfDocumentFields.orderColumnWidths);
                        }

                        if (documentType == PDFDocumentType.Invoice)
                        {
                            PdfPTable headerTable = new PdfPTable(PdfDocumentFields.INVOICE_HEADER_COLUMN_COUNT);
                            headerTable.SpacingBefore = 10.0f;
                            headerTable.WidthPercentage = 100.0f;
                            headerTable.SetWidths(PdfDocumentFields.invoiceColumnWidths);

                            cell1 = new PdfPCell(new Phrase("Kód", PdfDocumentFields.smallNormalFont));
                            cell1.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            cell2 = new PdfPCell(new Phrase("Název", PdfDocumentFields.smallNormalFont));
                            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell2.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            cell3 = new PdfPCell(new Phrase("Cena za kus", PdfDocumentFields.smallNormalFont));
                            cell3.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            cell4 = new PdfPCell(new Phrase("Množství", PdfDocumentFields.smallNormalFont));
                            cell4.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            cell5 = new PdfPCell(new Phrase("Cena celkem s DPH", PdfDocumentFields.smallNormalFont));
                            cell5.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;

                            headerTable.AddCell(cell1);
                            headerTable.AddCell(cell2);
                            headerTable.AddCell(cell3);
                            headerTable.AddCell(cell4);
                            headerTable.AddCell(cell5);

                            doc.Add(headerTable);

                            itemsTable = new PdfPTable(PdfDocumentFields.INVOICE_HEADER_COLUMN_COUNT);
                            itemsTable.SpacingBefore = 10.0f;
                            itemsTable.WidthPercentage = 100.0f;
                            itemsTable.SetWidths(PdfDocumentFields.invoiceColumnWidths);
                        }

                        if (documentType == PDFDocumentType.DeliveryNote)
                        {
                            PdfPTable headerTable = new PdfPTable(PdfDocumentFields.DELIVERY_NOTE_HEADER_COLUMN_COUNT);
                            headerTable.WidthPercentage = 100.0f;
                            headerTable.SetWidths(PdfDocumentFields.deliveryNoteHeaderTableColumnWidths);

                            if (PdfDocumentFields.pageNumber > 1)
                            {
                                headerTable.SpacingBefore = 10.0f;
                            }

                            numberCell = new PdfPCell(new Phrase("Poř. číslo", PdfDocumentFields.smallNormalFont));
                            numberCell.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
                            cell1 = new PdfPCell(new Phrase("Kód", PdfDocumentFields.smallNormalFont));
                            cell1.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
                            cell2 = new PdfPCell(new Phrase("Název", PdfDocumentFields.smallNormalFont));
                            cell2.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
                            cell2.PaddingLeft = 10.0f;
                            cell3 = new PdfPCell(new Phrase("Cena za kus", PdfDocumentFields.smallNormalFont));
                            cell3.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
                            cell4 = new PdfPCell(new Phrase("Množství", PdfDocumentFields.smallNormalFont));
                            cell4.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
                            cell5 = new PdfPCell(new Phrase("Cena celkem s DPH", PdfDocumentFields.smallNormalFont));
                            cell5.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;

                            headerTable.AddCell(numberCell);
                            headerTable.AddCell(cell1);
                            headerTable.AddCell(cell2);
                            headerTable.AddCell(cell3);
                            headerTable.AddCell(cell4);
                            headerTable.AddCell(cell5);

                            doc.Add(headerTable);

                            itemsTable = new PdfPTable(PdfDocumentFields.DELIVERY_NOTE_HEADER_COLUMN_COUNT);
                            itemsTable.SpacingBefore = 5.0f;
                            itemsTable.WidthPercentage = 100.0f;
                            itemsTable.SetWidths(PdfDocumentFields.deliveryNoteHeaderTableColumnWidths);
                        }

                        if (PdfDocumentFields.pageNumber == 2)
                        {
                            setFullItemsPerPageCount = true;
                        }

                        PdfDocumentFields.pageNumber += 1;
                    }

                    if (documentType == PDFDocumentType.DeliveryNote)
                    {
                        numberCell = new PdfPCell(new Phrase(PdfDocumentFields.sumaItemNumber.ToString(), PdfDocumentFields.smallNormalFont));
                        numberCell.BorderColor = BaseColor.LIGHT_GRAY;
                        numberCell.Border = Rectangle.BOTTOM_BORDER;
                        numberCell.MinimumHeight = 23.0f;
                    }

                    if (documentType == PDFDocumentType.Invoice || documentType == PDFDocumentType.DeliveryNote)
                    {
                        cell1 = new PdfPCell(new Phrase(item.Product.ID.ToString(), PdfDocumentFields.smallNormalFont));
                        cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell1.BorderColor = BaseColor.LIGHT_GRAY;
                        cell1.Border = Rectangle.BOTTOM_BORDER;
                        cell1.MinimumHeight = 23.0f;
                    }

                    cell2 = new PdfPCell(new Phrase(item.Product.Name, PdfDocumentFields.smallNormalFont));
                    cell2.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell2.BorderColor = BaseColor.LIGHT_GRAY;
                    cell2.Border = Rectangle.BOTTOM_BORDER;
                    cell2.MinimumHeight = 23.0f;
                    cell2.PaddingLeft = 10.0f;
                    cell3 = new PdfPCell(new Phrase(item.Product.Price.ToString(), PdfDocumentFields.smallNormalFont));
                    cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell3.BorderColor = BaseColor.LIGHT_GRAY;
                    cell3.Border = Rectangle.BOTTOM_BORDER;
                    cell3.MinimumHeight = 23.0f;
                    cell4 = new PdfPCell(new Phrase(item.Quantity.ToString(), PdfDocumentFields.smallNormalFont));
                    cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell4.BorderColor = BaseColor.LIGHT_GRAY;
                    cell4.Border = Rectangle.BOTTOM_BORDER;
                    cell4.MinimumHeight = 23.0f;
                    cell5 = new PdfPCell(new Phrase((item.Product.Price * item.Quantity).ToString(), PdfDocumentFields.smallNormalFont));
                    cell5.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell5.BorderColor = BaseColor.LIGHT_GRAY;
                    cell5.Border = Rectangle.BOTTOM_BORDER;
                    cell5.MinimumHeight = 23.0f;

                    if (documentType == PDFDocumentType.DeliveryNote)
                    {
                        itemsTable.AddCell(numberCell);
                    }

                    if (documentType == PDFDocumentType.Invoice || documentType == PDFDocumentType.DeliveryNote)
                    {
                        itemsTable.AddCell(cell1);
                    }

                    itemsTable.AddCell(cell2);
                    itemsTable.AddCell(cell3);
                    itemsTable.AddCell(cell4);
                    itemsTable.AddCell(cell5);

                    PdfDocumentFields.sumaPrice += item.Product.Price * item.Quantity;
                    PdfDocumentFields.itemNumber += 1;

                    if (PdfDocumentFields.itemNumber == PdfDocumentFields.itemsPerPageCount || PdfDocumentFields.sumaItemNumber == itemsCount)
                    {
                        doc.Add(itemsTable);
                        doc.Add(new Paragraph(new Chunk(PdfDocumentFields.lineSeparator)));

                        if (PdfDocumentFields.itemNumber == PdfDocumentFields.itemsPerPageCount && PdfDocumentFields.sumaItemNumber != itemsCount)
                        {
                            doc.NewPage();
                            PdfDocumentFields.itemNumber = 0;
                        }

                        if (PdfDocumentFields.sumaItemNumber == itemsCount)
                        {
                            PdfDocumentFields.sumaPrice += PdfDocumentFields.deliveryPrice;

                            paragraph = new Paragraph(new Chunk(PdfDocumentFields.horizontalSpace));
                            paragraph.Add(new Chunk("Cena za dopravu a dobírku:  " + PdfDocumentFields.deliveryPrice + " Kč", PdfDocumentFields.middleNormalFont));
                            doc.Add(paragraph);
                            doc.Add(new Paragraph(new Chunk(PdfDocumentFields.lineSeparator)));
                            paragraph = new Paragraph(new Chunk(PdfDocumentFields.horizontalSpace));
                            paragraph.Add(new Chunk("Celková cena s DPH:  " + PdfDocumentFields.sumaPrice + " Kč", PdfDocumentFields.middleBoldFont));
                            doc.Add(paragraph);
                            doc.Add(new Paragraph(new Chunk(PdfDocumentFields.sumaLineSeparator)));
                        }
                    }

                    PdfDocumentFields.sumaItemNumber += 1;

                    if (setFullItemsPerPageCount == true)
                    {
                        PdfDocumentFields.itemsPerPageCount = PdfDocumentFields.generalItemsPerPageCount;
                        setFullItemsPerPageCount = false;
                    }
                }

                if (doc.IsOpen())
                {
                    doc.Close();
                }

                pdfFilePath = PdfDocumentFields.FILE_PATH;

                return Result.Success("Pdf document was created successfully");
            }
            catch (Exception exception)
            {
                pdfFilePath = string.Empty;
                return Result.FatalFormat("In method OrderService.CreateInvoice was thrown exception: {0}.",
                                                                         exception.Message);
            }
            finally
            {
                if (doc.IsOpen())
                {
                    doc.Close();
                }                
            }
            
            /*
            pdfFilePath = string.Empty;
            return SharedLibs.DataContracts.Result.Fatal("Libraries missing, must be added.");
            */
        }        
    }
}
