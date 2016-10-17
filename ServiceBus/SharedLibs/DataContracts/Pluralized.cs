using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SharedLibs.DataContracts
{
    /// <summary>
    /// General pluralization of data contracts
    /// </summary>
    /// <typeparam name="T">Type to be pluralized</typeparam>
    [DataContract]
    public class Pluralized<T> : DTO, IPluralized<T> where T : DTO
    {
        /// <summary>
        /// Collection of items
        /// </summary>
        [DataMember]
        public List<T> Items { get; set; }
    }
}
