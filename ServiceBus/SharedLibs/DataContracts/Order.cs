using System;
using System.Runtime.Serialization;

namespace SharedLibs.DataContracts
{
    /// <summary>
    /// Order information
    /// </summary>
    [DataContract]
    public class Order : DTO
    {
        /// <summary>
        /// Order ID
        /// Every order must have unique Order ID
        /// </summary>
        [DataMember]
        public Guid Id { set; get; }

        /// <summary>
        /// Simple order number used during communication with customer
        /// </summary>
        [DataMember]
        public string OrderNumber { set; get; }

        /// <summary>
        /// Basket containing ordered items and campaigns
        /// </summary>
        [DataMember]
        public Basket Basket { get; set; }

        /// <summary>
        /// Contains information about customer
        /// </summary>
        [DataMember]
        public User User { set; get; }

        /// <summary>
        /// Address to which the products should be delivered
        /// NOTE: Billing address if different from delivery address should be filled in Billing Information
        /// </summary>
        [DataMember]
        public Address DeliveryAddress { get; set; }

        /// <summary>
        /// Billing information
        /// Informations about billing address, BIC and IBAN.
        /// NOTE: Instead BIC and IBAN can be use bank code and account number
        /// </summary>
        [DataMember]
        public BillingInformation BillingInformation { set; get; }

        /// <summary>
        /// Date of order
        /// Can be automatically set to current date
        /// </summary>
        [DataMember]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Wished date of delivery
        /// Can be automatically set to current date + 1
        /// </summary>
        [DataMember]
        public DateTime DeliveryDate { get; set; }

        /// <summary>
        /// Number of invoice used during pay transactions
        /// </summary>
        [DataMember]
        public string InvoiceNumber { set; get; }

        /// <summary>
        /// Order state
        /// Keeps Id of orderStatus with actual state
        /// </summary>
        [DataMember]
        public OrderState OrderState { set; get; }        
    }
}
