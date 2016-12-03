using System;
using ServiceBus.EntityModels;
using SharedLibs.DataContracts;
using System.Collections.Generic;

namespace ServiceBus
{
    /// <summary>
    /// Servers as an entry service for work with baskets.
    /// </summary>
    public class BasketService : IBasketService
    {
        /// <summary>
        /// Creates a new basket and returns itself
        /// </summary>
        /// <returns>data of the newly created basket</returns>
        public Result AddCampaign(Guid basketId, Guid campaignId)
        {
            return Result.Fatal("Not Implemented");
        }

        /// <summary>
        /// Adds product to basket. This results in creating basket items in the database.
        /// </summary>
        /// <param name="baketId">basket id</param>
        /// <param name="productId">product id</param>
        /// <returns>result information</returns>
        public Result AddProduct(Guid baketId, Guid productId)
        {
            return Result.Fatal("Not Implemented");
        }

        /// <summary>
        /// Adds product to basket. This results in creating basket items in the database.
        /// </summary>
        /// <param name="baketId">basket id</param>
        /// <param name="productId">product id</param>
        /// <param name="quantity">number of products to be added</param>
        /// <returns>result information</returns>
        public Result AddProduct(Guid baketId, Guid productId, int quantity)
        {
            return Result.Fatal("Not Implemented");
        }

        /// <summary>
        /// Removes product from basket. This results in removal of basket items from the database.
        /// </summary>
        /// <param name="baketId">basket id</param>
        /// <param name="productId">product id</param>
        /// <returns>result information</returns>
        public Result RemoveProduct(Guid baketId, Guid productId)
        {
            return Result.Fatal("Not Implemented");
        }

        /// <summary>
        /// Removes quantity of products from basket. This results in removal of basket items from the database.
        /// </summary>
        /// <param name="baketId">basket id</param>
        /// <param name="productId">product id</param>
        /// <param name="quantity">number of products to be added</param>
        /// <returns>result information</returns>
        public Result RemoveProduct(Guid baketId, Guid productId, int quantity)
        {
            return Result.Fatal("Not Implemented");
        }

        /// <summary>
        /// Adds campaing into the basket. This results in creation of basket campaigns in the database.
        /// </summary>
        /// <param name="basketId">basket id</param>
        /// <param name="campaignId">camaping id</param>
        /// <returns>result information</returns>
        public SharedLibs.DataContracts.Basket CreateBasket()
        {
            try
            {
                using (var context = new EntityModels.ServiceBusDatabaseEntities())
                    {
                    EntityModels.Basket b = context.Baskets.Add(new EntityModels.Basket() {
                            Id = Guid.NewGuid()
                        });
                        context.SaveChanges();
                        b = context.Baskets.Attach(b);
                        return new SharedLibs.DataContracts.Basket()
                        {
                            Id = b.Id,
                            Result = SharedLibs.DataContracts.Result.SuccessFormat("Basket {0} was created.", b.Id)
                        };
                        
                    }
                
               
            }
            catch (Exception ex)
            {
                throw new Exception("Creation of basket failed. "+ex.Message);
            }
            
        }

        /// <summary>
        /// Returns complete basket
        /// </summary>
        /// <param name="basket id"></param>
        /// <returns>basket with reqested id</returns>
        public SharedLibs.DataContracts.Basket GetBasket(Guid basketId)
        {
            try
            {
                using (var context = new EntityModels.ServiceBusDatabaseEntities())
                {
                    var basket = context.Baskets.Find(basketId);
                    return new SharedLibs.DataContracts.Basket()
                    {
                        Id = basketId,
                       // BasketItems = (List<SharedLibs.DataContracts.BasketItem>)basket.BasketItems,
                        Result = SharedLibs.DataContracts.Result.SuccessFormat("Basket {0} found.", basketId)
                    };
                }
            }
            catch (Exception ex)
            {
                //Console.Write(ex.Message);
                return null;
            }
        }
    }
}
