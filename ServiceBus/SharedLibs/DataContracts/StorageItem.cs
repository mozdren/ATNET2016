using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibs.DataContracts
{
    /// <summary>
    /// Datacontract for records about repair actions
    /// </summary>
    [DataContract]
    public class StorageItem : DTO
    {
        //TODO: Only identifiers are kept. I am not sure if EF object is correct (it has product Guid as well as Product object for example).

        /// <summary>
        /// Identifier of a stored item
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Numer of units in stock
        /// </summary>
        [DataMember]
        public int Quantity { get; set; }

        /// <summary>
        /// Product identifier
        /// </summary>
        [DataMember]
        public Guid ProductId { get; set; }
        
        /// <summary>
        /// Identifier of storage where given stock is kept
        /// </summary>
        [DataMember]
        public Guid StorageId { get; set; }
    }
}
