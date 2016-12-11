using System.ServiceModel;
using System;
using System.Web.Services;
using SharedLibs.DataContracts;
using SharedLibs.Enums;

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
        Product GetProduct(Guid guid);

        /// <summary>
        /// Returns all products
        /// </summary>
        /// <returns>collection of all products</returns>
        [OperationContract]
        Products GetAllProducts();

        /// <summary>
        /// This method adds product into datasource
        /// </summary>
        /// <param name="name">Name of a new product</param>
        /// <param name="price">Price of a new product</param>
        /// <param name="guid">ID of a new prodcut</param>
        /// <param name="headliner">Specifies if a product is a main product</param>
        /// <param name="productType">Type of a product</param>
        /// <returns>Result object</returns>
        [OperationContract (Name = "AddProduct")]
        Result AddProduct(string name, double price, Guid guid, ProductTypes productType, bool headliner);

        /// <summary>
        /// This method edits product in case product exists
        /// </summary>
        /// <param name="guid">ID of a product</param>
        /// <param name="name">New name for a product</param>
        /// <param name="price">New price for a product</param>
        /// <returns>Modified product</returns>
        [OperationContract]
        Product EditProduct(Guid guid, string name, double price);

        /// <summary>
        /// Delete product item from datasource
        /// </summary>
        /// <param name="guid">ID of a product</param>
        /// <returns>Result object</returns>
        [OperationContract]
        Result DeleteProduct(Guid guid);
    }
}
