using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibs.Enums
{
    /// <summary>
    /// Categories for product. Each product has to belong to one category
    /// </summary>
    [DataContract]
    public enum ProductTypes
    {
        /// <summary>
        /// Product doesn't have special category
        /// </summary>
        [EnumMember]
        Miscellaous = 1,

        /// <summary>
        /// Do it yourself category
        /// </summary>
        [EnumMember]
        DIY,

        /// <summary>
        /// Stuff related to computers and IT in general
        /// </summary>
        [EnumMember]
        Computers,

        /// <summary>
        /// All kinds electronic appliances except IT stuff
        /// </summary>
        [EnumMember]
        Electronics,

        /// <summary>
        /// Clothes, shoes, jewelery
        /// </summary>
        [EnumMember]
        Cloathing,

        /// <summary>
        /// Books, magazines, e-books and similiar content
        /// </summary>
        [EnumMember]
        Books,

        /// <summary>
        /// Equipment for sport
        /// </summary>
        [EnumMember]
        Sports,

        /// <summary>
        /// Category 18+
        /// </summary>
        [EnumMember]
        Adults

    }
}
