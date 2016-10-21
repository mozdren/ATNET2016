using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SharedLibs.DataContracts
{
    [DataContract]
    public class Basket : DTO
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public List<BasketItem> BasketItems { get; set; }

        [DataMember]
        public List<Campaign> BasketCampaings { get; set; }
    }
}
