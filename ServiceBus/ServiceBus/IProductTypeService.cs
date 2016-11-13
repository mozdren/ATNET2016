using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceBus
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProductTypeService" in both code and config file together.
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
        /// <param name="productTypeId">Identifier of a product</param>
        /// <returns></returns>
        [OperationContract]
        SharedLibs.DataContracts.ProductType GetProductType(int productTypeId);
    }
}
