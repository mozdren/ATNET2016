using System.Runtime.Serialization;

namespace SharedLibs.Enums
{
    /// <summary>
    /// PDF document types
    /// </summary>
    [DataContract]
    public enum PDFDocumentType
    {
        /// <summary>
        /// PDF document type order
        /// </summary>
        [EnumMember]
        Order = 0,

        /// <summary>
        /// PDF document type invoice
        /// </summary>
        [EnumMember]
        Invoice,

        /// <summary>
        /// PDF document type delivery note
        /// </summary>
        [EnumMember]
        DeliveryNote
    }
}
