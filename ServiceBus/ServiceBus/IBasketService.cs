using System;
using System.ServiceModel;
using SharedLibs.DataContracts;

namespace ServiceBus
{
    /// <summary>
    /// Servers as an entry service for work with baskets.
    /// </summary>
    [ServiceContract]
    public interface IBasketService
    {
        /// <summary>
        /// Creates a new basket and returns itself
        /// </summary>
        /// <returns>data of the newly created basket</returns>
        [OperationContract]
        Basket CreateBasket();

        /// <summary>
        /// Adds product to basket. This results in creating basket items in the database.
        /// </summary>
        /// <param name="baketId">basket id</param>
        /// <param name="productId">product id</param>
        /// <returns>result information</returns>
        [OperationContract]
        Result AddProduct(Guid baketId, Guid productId);

        /// <summary>
        /// Adds product to basket. This results in creating basket items in the database.
        /// </summary>
        /// <param name="baketId">basket id</param>
        /// <param name="productId">product id</param>
        /// <param name="quantity">number of products to be added</param>
        /// <returns>result information</returns>
        [OperationContract(Name = "AddProductWithQuantity")]
        Result AddProduct(Guid baketId, Guid productId, int quantity);

        /// <summary>
        /// Removes product from basket. This results in removal of basket items from the database.
        /// </summary>
        /// <param name="baketId">basket id</param>
        /// <param name="productId">product id</param>
        /// <returns>result information</returns>
        [OperationContract]
        Result RemoveProduct(Guid baketId, Guid productId);

        /// <summary>
        /// Removes quantity of products from basket. This results in removal of basket items from the database.
        /// </summary>
        /// <param name="baketId">basket id</param>
        /// <param name="productId">product id</param>
        /// <param name="quantity">number of products to be added</param>
        /// <returns>result information</returns>
        [OperationContract(Name = "RemoveProductWithQuantity")]
        Result RemoveProduct(Guid baketId, Guid productId, int quantity);

        /// <summary>
        /// Adds campaing into the basket. This results in creation of basket campaigns in the database.
        /// </summary>
        /// <param name="basketId">basket id</param>
        /// <param name="campaignId">camaping id</param>
        /// <returns>result information</returns>
        [OperationContract]
        Result AddCampaign(Guid basketId, Guid campaignId);

        /// <summary>
        /// Returns complete basket
        /// </summary>
        /// <param name="basket id"></param>
        /// <returns>basket with reqested id</returns>
        [OperationContract]
        Basket GetBasket(Guid basketId);
    }
}
