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
        /// Business Identifier Code (or also a bank code)
        /// </summary>
        [DataMember]
        public string BIC { get; set; }

        /// <summary>
        /// International bank anccount number (or simply an account number)
        /// </summary>
        [DataMember]
        public string IBAN { get; set; }
    }
}
