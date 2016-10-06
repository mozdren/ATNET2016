using System.Runtime.Serialization;

namespace SharedLibs.DataContracts
{
    /// <summary>
    /// Basic class for datacontracts
    /// </summary>
    [DataContract]
    public class DTO
    {
        /// <summary>
        /// Each datacontract should have result, even when used as a parameter of a method.
        /// In that case it is always set to null. If returned as a result of a call, then
        /// the result should be properly initialized with relevant information.
        /// </summary>
        [DataMember]
        public Result Result { get; set; }
    }
}
