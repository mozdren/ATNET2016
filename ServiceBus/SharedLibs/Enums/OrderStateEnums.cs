using System.Runtime.Serialization;

namespace SharedLibs.Enums
{
    /// <summary>
    /// Order state types
    /// </summary>
    [DataContract]
    public enum OrderStateType
    {
        /// <summary>
        /// State the order was canceled
        /// </summary>
        [EnumMember]
        Canceled = 0,


        /// <summary>
        /// State the order was created
        /// </summary>
        [EnumMember]
        Created,


        /// <summary>
        /// State the order was changed
        /// </summary>
        [EnumMember]
        Changed,


        /// <summary>
        /// State the order was finished
        /// </summary>
        [EnumMember]
        Finished,


        /// <summary>
        /// State the order was paid
        /// </summary>
        [EnumMember]
        Paid
    }
}