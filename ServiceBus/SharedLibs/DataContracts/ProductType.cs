using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibs.DataContracts
{
    [DataContract]
    public class ProductType : DTO
    {
        /// <summary>
        /// Identifier of the product type
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// Name of a product type
        /// </summary>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// All products of this product type
        /// </summary>
        [DataMember]
        public Products Products{ get; set; }


        /// <summary>
        /// All campaigns aimed at this product
        /// </summary>
        [DataMember]
        public Campaigns Campaings { get; set; }
    }
}
