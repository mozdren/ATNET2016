using System;
using System.Runtime.Serialization;

namespace SharedLibs.DataContracts
{
    /// <summary>
    /// Datacontract for product
    /// </summary>
    [DataContract]
    public class Product : DTO
    {
        /// <summary>
        /// Identifier of the product
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        [DataMember]
        public double? Price { get; set; }

        /// <summary>
        /// States if product is marked as valid or not
        /// </summary>
        [DataMember]
        public bool Enabled { get; set; }

        /// <summary>
        /// States if product is a main product
        /// </summary>
        [DataMember]
        public bool Headliner { get; set; }

        /// <summary>
        /// Product type of the product
        /// </summary>
        [DataMember]
        public ProductType ProductType { get; set; }

        /// <summary>
        /// All basket items with this specific product
        /// </summary>
        [DataMember]
        public BasketItems BasketItems { get; set; }

        /// <summary>
        /// All repairs of this product in evidence
        /// </summary>
        [DataMember]
        public Repairs Repairs { get; set; }

        /// <summary>
        /// All reservations made for this product
        /// </summary>
        [DataMember]
        public Reservations Reservations { get; set; }

        /// <summary>
        /// All items in store of this specific product kind
        /// </summary>
        [DataMember]
        public StorageItems StorageItems { get; set; }
    }
}
