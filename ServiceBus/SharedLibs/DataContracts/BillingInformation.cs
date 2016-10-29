using System;
using System.Runtime.Serialization;

namespace SharedLibs.DataContracts
{
    /// <summary>
    /// Information for Billing
    /// </summary>
    [DataContract]
    public class BillingInformation : DTO
    {
        /// <summary>
        /// Billing Information ID
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Address used for billing
        /// </summary>
        [DataMember]
        public Address BillingAddress { get; set; }

        /// <summary>
        /// Business Identifier Code - in case the payment is performed in Czech republic
        /// it may be inserted bank code (example: 0300 -> csob, 0800 -> ceska sporitelna, atc.)
        /// </summary>
        [DataMember]
        public string BIC { get; set; }

        /// <summary>
        /// International bank anccount number - in case the payment is performed in Czech republic 
        /// it may be inserted account number (example: 1234567890, 123456-1234567890)
        /// </summary>
        [DataMember]
        public string IBAN { get; set; }
    }
}
