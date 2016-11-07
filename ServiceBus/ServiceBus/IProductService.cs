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
        /// This method adds product into datasource. Defaultly product is not main product.
        /// </summary>
        /// <param name="name">Name of a new product</param>
        /// <param name="price">Price of a new product</param>
        /// <param name="guid">ID of a new prodcut</param>
        /// <param name="pType">ID of a product type</param>
        /// <param name="headliner">Is this product supposed to be main product?</param>
        /// <returns>Result object</returns>
        [OperationContract (Name = "AddProduct")]
        SharedLibs.DataContracts.Result AddProduct(string name, double price, Guid guid, int pType, bool headliner);

        /// <summary>
        /// This method adds product into the datasource. Defaultly product is not main product.
        /// </summary>
        /// <param name="product">Product object</param>
        /// <param name="pType">ID of a product type</param>
        /// <param name="headliner">Is this product supposed to be main product?</param>
        /// <returns>Result object</returns>
        [OperationContract (Name = "AddProductByObject")]
        SharedLibs.DataContracts.Result AddProduct(SharedLibs.DataContracts.Product product, int pType, bool headliner);

        /// <summary>
        /// This method edits product in case product exists
        /// </summary>
        /// <param name="guid">ID of a product</param>
        /// <param name="name">New name for a product</param>
        /// <param name="price">New price for a product</param>
        /// <param name="headliner">Is this product supposed to be main product?</param>
        /// <param name="pType">Type of a product</param>
        /// <param name="enabled">Is the product still available for use?</param>
        /// <returns>Modified product</returns>
        [OperationContract]
        SharedLibs.DataContracts.Product EditProduct(Guid guid, string name, double price, ProductType pType, bool headliner, bool enabled);

        /// <summary>
        /// Delete product item from datasource
        /// </summary>
        /// <param name="guid">ID of a product</param>
        /// <returns>Result object</returns>
        [OperationContract]
        SharedLibs.DataContracts.Result DeleteProduct(Guid guid);
    }
}
