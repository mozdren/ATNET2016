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
    public class Reservation : DTO
    {
        //TODO: Only identifiers are kept. I am not sure if EF object is correct (it has product Guid as well as Product object for example).

        /// <summary>
        /// Identifier of an reservation record
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Specifies how many units are reserved for this particular reservation
        /// </summary>
        [DataMember]
        public int Count { get; set; }

        /// <summary>
        /// Product identifier
        /// </summary>
        [DataMember]
        public Guid ProductId { get; set; }
        
        /// <summary>
        /// Identifier of the user who issued request for a reservation of a specific product
        /// </summary>
        [DataMember]
        public Guid UserId { get; set; }

        /// <summary>
        /// Identifier of storage which is supposed to book requested quantity of specific product
        /// </summary>
        [DataMember]
        public Guid StorageId { get; set; }
    }
}
