using System.ServiceModel;
using System;

namespace ServiceBus
{
    /// <summary>
    /// This is a product service, which should serve as an entrypoint for everything that has
    /// something directly in common with products.
    /// </summary>
    [ServiceContract]
    public interface IProductService
    {
        /// <summary>
        /// Returns product with specific Guid
        /// </summary>
        /// <param name="guid">guid of a product</param>
        /// <returns>requested product</returns>
        [OperationContract]
        SharedLibs.DataContracts.Product GetProduct(Guid guid);

        /// <summary>
        /// Returns all products
        /// </summary>
        /// <returns>collection of all products</returns>
        [OperationContract]
        SharedLibs.DataContracts.Products GetAllProducts();
    }
}
