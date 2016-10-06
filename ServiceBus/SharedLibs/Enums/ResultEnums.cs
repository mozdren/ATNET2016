using System.Runtime.Serialization;

namespace SharedLibs.Enums
{
    /// <summary>
    /// Result Types ordered by severity
    /// </summary>
    [DataContract]
    public enum ResultType
    {
        /// <summary>
        /// Result of a method call was success
        /// </summary>
        [EnumMember]
        Success = 0,

        /// <summary>
        /// Result succeded, but there something insignificant went wrong
        /// </summary>
        [EnumMember]
        Warning,

        /// <summary>
        /// Response from a method call ended in an error
        /// </summary>
        [EnumMember]
        Error,

        /// <summary>
        /// Fatal Error Emerged (for example exception in the code)
        /// </summary>
        [EnumMember]
        Fatal
    }
}
