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
        /// Basket containing ordered items and campaigns
        /// </summary>
        [DataMember]
        public Basket Basket { get; set; }

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
        /// Order state
        /// Keeps actual state of order - implicit value NoState which is used in case there is no state
        /// </summary>
        [DataMember]
        public SharedLibs.Enums.OrderStateType OrderState { set; get; }
    }
}
