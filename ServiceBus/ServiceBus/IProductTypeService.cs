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
    /// something directly in common with products types.
    /// Once again this is for product TYPEs.
    /// </summary>
    [ServiceContract]
    public interface IProductTypeService
    {
        /// <summary>
        /// Adds new product type
        /// </summary>
        /// <param name="typeName">Name for a new type</param>
        /// <returns></returns>
        [OperationContract]
        SharedLibs.DataContracts.Result AddProductType(string typeName);

        /// <summary>
        /// Gets product type by its ID
        /// </summary>
        /// <param name="productTypeE">Identifier of a product</param>
        /// <returns></returns>
        [OperationContract]
        SharedLibs.DataContracts.ProductType GetProductType(ProductTypes productTypeE);
    }
}
