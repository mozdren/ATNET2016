using System;
using System.Runtime.Serialization;

namespace SharedLibs.DataContracts
{
    /// <summary>
    /// General Data Contract for Address
    /// </summary>
    [DataContract]
    public class Address : DTO
    {
        /// <summary>
        /// Adress ID
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Postal Code
        /// </summary>
        [DataMember]
        public string PostCode { get; set; }

        /// <summary>
        /// House Number
        /// </summary>
        [DataMember]
        public int HouseNumber { get; set; }

        /// <summary>
        /// House number extension can have alphabethical chracters
        /// </summary>
        [DataMember]
        public string HouseNumberExtension { get; set; }

        /// <summary>
        /// Street
        /// </summary>
        [DataMember]
        public string Street { get; set; }

        /// <summary>
        /// City
        /// </summary>
        [DataMember]
        public string City { get; set; }

        /// <summary>
        /// District - e.g. Praha 10
        /// </summary>
        [DataMember]
        public string District { get; set; }

        /// <summary>
        /// Door Number - this has to be included in some cases so the order reaches its customer
        /// </summary>
        [DataMember]
        public string DoorNumber { get; set; }
    }
}
