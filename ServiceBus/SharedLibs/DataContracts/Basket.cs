using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SharedLibs.DataContracts
{
    [DataContract]
    public class Basket : DTO
    {
        /// <summary>
        /// Basket identifier
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Basket items
        /// </summary>
        [DataMember]
        public List<BasketItem> BasketItems { get; set; }

        /// <summary>
        /// Basket campaigns - discounts
        /// </summary>
        [DataMember]
        public List<Campaign> BasketCampaings { get; set; }
    }
}
