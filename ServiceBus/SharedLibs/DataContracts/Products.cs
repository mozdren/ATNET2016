using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SharedLibs.DataContracts
{
    /// <summary>
    /// Datacontract representing a collection of products
    /// </summary>
    [DataContract]
    public class Products : DTO
    {
        /// <summary>
        /// Collection of products
        /// </summary>
        [DataMember]
        public List<Product> Items { get; set; }
    }
}
