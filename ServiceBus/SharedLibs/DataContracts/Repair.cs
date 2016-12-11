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
    public class Repair : DTO
    {
        //TODO: Only identifiers are kept. I am not sure if EF object is correct (it has product Guid as well as Product object).

        /// <summary>
        /// Repair identifier
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// Product identifier
        /// </summary>
        [DataMember]
        public Guid ProductId { get; set; }
        /// <summary>
        /// Serial number that identifes unique product 
        /// </summary>
        [DataMember]
        public string Serial { get; set; }
        /// <summary>
        /// Few words kept to describe and better understand of product possible malfunction
        /// </summary>
        [DataMember]
        public string Description { get; set; }
        /// <summary>
        /// Identifier of the user who raised request for a repair
        /// </summary>
        [DataMember]
        public Guid UserId { get; set; }
        /// <summary>
        /// Identifier of storage where product is kept till repair request is resolved
        /// </summary>
        [DataMember]
        public Guid StorageId { get; set; }
    }
}
