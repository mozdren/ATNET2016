using System;
using System.Runtime.Serialization;

namespace SharedLibs.DataContracts
{
    /// <summary>
    /// Keeps information about order state
    /// </summary>
    [DataContract]
    public class OrderState : DTO
    {
        /// <summary>
        /// Inique number of order status
        /// </summary>
        [DataMember]
        public Guid Id { set; get; }

        /// <summary>
        /// Status of order
        /// </summary>
        [DataMember]
        public int Status { set; get; }
    }
}
