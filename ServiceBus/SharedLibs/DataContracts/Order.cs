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
        /// </summary>
        [DataMember]
        public BillingInformation BillingInformation { set; get; }

        /// <summary>
        /// Date of order
        /// </summary>
        [DataMember]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Wished date of delivery
        /// </summary>
        [DataMember]
        public DateTime DeliveryDate { get; set; }
    }
}
