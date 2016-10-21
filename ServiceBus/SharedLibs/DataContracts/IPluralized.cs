using System.Collections.Generic;

namespace SharedLibs.DataContracts
{
    /// <summary>
    /// Interface of pluralized data contracts
    /// </summary>
    /// <typeparam name="T">Type of data contract to be used</typeparam>
    public interface IPluralized<T> where T : DTO
    {
        /// <summary>
        /// Collection of data contracts
        /// </summary>
        List<T> Items { get; set; }
    }
}
