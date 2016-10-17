using System;
using System.Runtime.Serialization;

namespace SharedLibs.DataContracts
{
    /// <summary>
    /// Represents basket item - An item to be added to basket
    /// </summary>
    [DataContract]
    public class BasketItem: DTO
    {
        /// <summary>
        /// Id of a basket item (not the id of a product)
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Product associated with the basket item
        /// </summary>
        [DataMember]
        public Product Product { get; set; }

        /// <summary>
        /// Quantity of the products
        /// </summary>
        [DataMember]
        public int Quantity { get; set; }
    }
}
