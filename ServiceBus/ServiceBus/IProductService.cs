using System.ServiceModel;
using System;
using System.Web.Services;

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

        /// <summary>
        /// This method adds product into datasource
        /// </summary>
        /// <param name="name">Name of a new product</param>
        /// <param name="price">Price of a new product</param>
        /// <param name="guid">ID of a new prodcut</param>
        /// <returns>Result object</returns>
        [OperationContract (Name = "AddProduct")]
        SharedLibs.DataContracts.Result AddProduct(string name, double price, Guid guid);

        /// <summary>
        /// This method adds product into the datasource
        /// </summary>
        /// <param name="product">Product object</param>
        /// <returns>Result object</returns>
        [OperationContract (Name = "AddProductByObject")]
        SharedLibs.DataContracts.Result AddProduct(SharedLibs.DataContracts.Product product);
    }
}
