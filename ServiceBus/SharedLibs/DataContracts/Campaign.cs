using System;
using System.Runtime.Serialization;

namespace SharedLibs.DataContracts
{
    /// <summary>
    /// Campaign providing discount by decreasing the total price
    /// </summary>
    [DataContract]
    public class Campaign : DTO
    {
        /// <summary>
        /// Id of the campaign
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the campaing
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Discount provided by the campaing
        /// </summary>
        [DataMember]
        public double Discount { get; set; }
    }
}
