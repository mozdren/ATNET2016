using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SharedLibs.Enums;

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
        public double Price { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        [DataMember]
        public ProductType ProductType { get; set; }

        /// <summary>
        /// Determines whether product is still valid 
        /// </summary>
        [DataMember]
        public bool Enabled { get; set; }

        /// <summary>
        /// Headliner is only for main products 
        /// </summary>
        [DataMember]
        public bool Headliner { get; set; }

        /// <summary>
        /// All items added to basket of this specific product 
        /// </summary>
        [DataMember]
        public List<BasketItem> BasketItems { get; set; }

        /// <summary>
        /// All items under repair process of this specific product
        /// </summary>
        [DataMember]
        public List<Repair> Repairs { get; set; }

        /// <summary>
        /// All reservation of this specific product
        /// </summary>
        [DataMember]
        public List<Reservation> Reservations { get; set; }

        /// <summary>
        /// Records about total stock of this specific product
        /// </summary>
        [DataMember]
        public List<StorageItem> StorageItems { get; set; }
    }
}
