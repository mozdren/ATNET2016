using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using SharedLibs.Enums;

namespace ServiceBus
{
    /// <summary>
    /// This is a product type service, which should serve as an entrypoint for everything that has
    /// something directly in common with product types.
    /// </summary>
    [ServiceContract]
    public interface IProductTypeService
    {
        /// <summary>
        /// Method to get product type DTO by its ID
        /// </summary>
        /// <param name="guid">Product type identifier</param>
        /// <returns>Product Type DTO</returns>
        [OperationContract]
        SharedLibs.DataContracts.ProductType GetProdcutType(Guid guid);
    }
}
