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
        /// State the order is ready to send to customer
        /// </summary>
        [EnumMember]
        ReadyToSend,


        /// <summary>
        /// State the order was sent to customer
        /// </summary>
        [EnumMember]
        Sent,


        /// <summary>
        /// State the order was paid
        /// </summary>
        [EnumMember]
        Paid,


        /// <summary>
        /// State the order was finished
        /// </summary>
        [EnumMember]
        Finished        
    }
}