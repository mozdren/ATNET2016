﻿using System;
using System.Runtime.Serialization;

namespace SharedLibs.DataContracts
{
    /// <summary>
    /// User information
    /// </summary>
    [DataContract]
    public class User : DTO
    {
        /// <summary>
        /// Unique user ID
        /// </summary>
        [DataMember]
        public Guid Id { set; get; }


        /// <summary>
        /// Login of user - used for registered users
        /// It can be null for anonymous users
        /// </summary>
        [DataMember]
        public string Login { set; get; }


        /// <summary>
        /// Password of user - used for registered users
        /// It can be null for anonymous users
        /// </summary>
        [DataMember]
        public string Password { set; get; }


        /// <summary>
        /// Name of user
        /// </summary>
        [DataMember]
        public string Name { set; get; }


        /// <summary>
        /// Surname of user
        /// </summary>
        [DataMember]
        public string Surname { set; get; }


        /// <summary>
        /// Name of company
        /// If the customer is company
        /// </summary>
        [DataMember]
        public string CompanyName { set; get; }


        /// <summary>
        /// E-mail address
        /// </summary>
        [DataMember]
        public string EmailAddress { set; get; }


        /// <summary>
        /// Phone number
        /// </summary>
        [DataMember]
        public string PhoneNumber { set; get; }


        /// <summary>
        /// Identification number of company
        /// </summary>
        [DataMember]
        public string IdentificationCompanyNumber { set; get; }


        /// <summary>
        /// Tax Identification number of company
        /// </summary>
        [DataMember]
        public string TaxIdentificationCompanyNumber { set; get; }


        /// <summary>
        /// Address of user
        /// </summary>
        [DataMember]
        public Address UserAddress { set; get; }
    }
}
