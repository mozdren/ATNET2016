using System;
using SharedLibs.DataContracts;
using System.Linq;
using System.Collections.Generic;
using SharedLibs.Enums;

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
        public Basket CreateBasket()
        {

            try
            {
                using (var context = new EntityModels.ServiceBusDatabaseEntities())
                {
                    var basket = new Basket() {Id= Guid.NewGuid(), Result= Result.Success("New Basket created.") };
                    
                    context.Baskets.Add(new EntityModels.Basket() { Id = basket.Id, TotalPrice = 0 });
                    context.SaveChanges();
                 
                    return basket;
                }
                
            }
            catch (Exception exception)
            {
                return new Basket { Result = Result.ErrorFormat("BasketService.CreateBasket Exception: {0}", exception.Message) };
            }

            
        }

        /// <summary>
        /// Returns complete basket
        /// </summary>
        /// <param name="basket id"></param>
        /// <returns>basket with reqested id</returns>
        public Basket GetBasket(Guid basketId)
        {


            try
            {
                using (var context = new EntityModels.ServiceBusDatabaseEntities())
                {
                    var basket = context.Baskets.FirstOrDefault(b => b.Id == basketId);

                    if (basket == null)
                    {
                        return new Basket
                        {
                            Result = Result.ErrorFormat("Basket {0} not found.", basketId)
                        };
                    }


                    var listBasketItems = new List<BasketItem>();

                    using (var productService = new ProductServiceProxyClass.ProductServiceClient())
                    {
                        foreach (var basketItem in basket.BasketItems)
                        {
                            listBasketItems.Add(

                                new BasketItem()
                                {
                                    Id = basketItem.Id,
                                    Product = productService.GetProduct(basketItem.Product.Id),
                                //Product = new Product() { ID = basketItem.Product.Id, Name = basketItem.Product.Name, Price = basketItem.Product.Price.HasValue ? basketItem.Product.Price.Value : 0.0 },
                                Quantity = basketItem.Quantity
                                }
                            );
                        }
                    }

                    

                    

                    var listCampaignItems = new List<Campaign>();

                    foreach (var campaignItem in basket.CampaignItems)
                    {
                        listCampaignItems.Add(

                            new Campaign()
                            {
                                Id = campaignItem.Campaign.Id,
                                Name = campaignItem.Campaign.Name,
                                Discount = campaignItem.Campaign.Discount.HasValue ? campaignItem.Campaign.Discount.Value : 0.0
                            }
                        );
                    }



                    var returnBasket = new Basket()
                    {
                        Result = Result.SuccessFormat("Basket {0} found", basketId),
                        Id = basketId,
                        BasketItems = listBasketItems,
                        BasketCampaings = listCampaignItems
                    };

                    return returnBasket;
                }
            }
            catch (Exception exception)
            {
                return new Basket
                {
                    Result = Result.FatalFormat("BasketService.GetBasket Exception: {0}", exception.Message)
                };
            }
        }

        /// <summary>
        /// Adds campaing into the basket. This results in creation of basket campaigns in the database.
        /// </summary>
        /// <param name="basketId">basket id</param>
        /// <param name="campaignId">camaping id</param>
        /// <returns>result information</returns>
        public Result AddCampaign(Guid basketId, Guid campaignId)
        {
            try
            {
                using (var context = new EntityModels.ServiceBusDatabaseEntities())
                {
                    var basket = context.Baskets.FirstOrDefault(p => p.Id == basketId);
                    if (basket == null)
                    {
                        return Result.Error("Basket was not found. Please make sure basket ID is valid");
                    }

                    var campaign = context.Campaigns.FirstOrDefault(c => c.Id == campaignId);
                    if (campaign == null)
                    {
                        return Result.Error("Campaign was not found. Please make sure campaign ID is valid");
                    }

                    var campaignItem = basket.CampaignItems.FirstOrDefault(p => p.CampaignId == campaignId);

                    

                    if (campaignItem == null)
                    {
                        context.CampaignItems.Add(new EntityModels.CampaignItem()
                        {
                            CampaignId = campaignId,
                            BasketId = basketId,
                            Id = Guid.NewGuid()
                        });

                        context.SaveChanges();

                        return Result.SuccessFormat("Campaign has been added: ");
                    }
                    else
                    {
                        return Result.Success("Campaign is allready in the basket");
                    }
                }
            }
            catch (Exception exception)
            {
                return Result.FatalFormat("BasketService.AddProduct Exception: {0}", exception.Message);
            }
        }

        /// <summary>
        /// Adds product to basket. This results in creating basket items in the database.
        /// </summary>
        /// <param name="basketId">basket id</param>
        /// <param name="productId">product id</param>
        /// <returns>result information</returns>
        public Result AddProduct(Guid basketId, Guid productId)
        {
            return AddProduct(basketId, productId, 1);
        }

        /// <summary>
        /// Adds product to basket. This results in creating basket items in the database.
        /// </summary>
        /// <param name="basketId">basket id</param>
        /// <param name="productId">product id</param>
        /// <param name="quantity">number of products to be added</param>
        /// <returns>result information</returns>
        public Result AddProduct(Guid basketId, Guid productId, int quantity)
        {
            try
            {
                if (quantity <= 0)
                {
                    return Result.Error("Provided parameter quantity must be bigger then 0.");
                }

                using (var context = new EntityModels.ServiceBusDatabaseEntities())
                {
                    var basket = context.Baskets.FirstOrDefault(p => p.Id == basketId);
                    if (basket == null)
                    {
                        return Result.Error("Basket was not found. Please make sure basket ID is valid");
                    }

                    var product = context.Products.FirstOrDefault(p => p.Id == productId);
                    if (product == null)
                    {
                        return Result.Error("Product was not found. Please make sure product ID is valid");
                    }

                    var basketItem = basket.BasketItems.FirstOrDefault(p => p.ProductId == productId);

                    if (basketItem == null)
                    {
                        context.BasketItems.Add(new EntityModels.BasketItem()
                        {
                            ProductId = productId,
                            BasketId = basketId,
                            Quantity=quantity,
                            Id = Guid.NewGuid()
                        });

                        context.SaveChanges();

                        return Result.SuccessFormat("Product has been added to basket");
                    }
                    else  //Product exists in the basket - update BasketItems
                    {                      
                        basketItem.Quantity = basketItem.Quantity + quantity;
                        context.SaveChanges();

                        return Result.SuccessFormat("Product has been increes");
                    }

                }                   
            }
            catch (Exception exception)
            {
                return Result.FatalFormat("BasketService.AddProduct Exception: {0}", exception.Message);
            }
        }

        /// <summary>
        /// Removes product from basket. This results in removal of basket items from the database.
        /// </summary>
        /// <param name="basketId">basket id</param>
        /// <param name="productId">product id</param>
        /// <returns>result information</returns>
        public Result RemoveProduct(Guid basketId, Guid productId)
        {
            return RemoveProduct(basketId, productId, 1);
        }

        /// <summary>
        /// Removes quantity of products from basket. This results in removal of basket items from the database.
        /// </summary>
        /// <param name="basketId">basket id</param>
        /// <param name="productId">product id</param>
        /// <param name="quantity">number of products to be added</param>
        /// <returns>result information</returns>
        public Result RemoveProduct(Guid basketId, Guid productId, int quantity)
        {
            try
            {
                using (var context = new EntityModels.ServiceBusDatabaseEntities())
                {
                    var basket = context.Baskets.FirstOrDefault(p => p.Id == basketId);
                    if (basket == null)
                    {
                        return Result.Error("Basket was not found. Please make sure basket ID is valid");
                    }
                    var basketItem = basket.BasketItems.FirstOrDefault(p => p.ProductId == productId);
                    if (basketItem == null)
                    {
                        return Result.Error("Product was not found. Please make sure product ID is valid");
                    }
                    if (quantity <= 0)
                    {
                        return Result.Error("Provided parameter quantity must be bigger then 0.");
                    }
                    if (quantity > basketItem.Quantity)
                    {
                        return Result.Error("Provided parameter quantity is too high.");
                    }

                    if (quantity == basketItem.Quantity)
                    {
                        context.BasketItems.Remove(basketItem);
                        context.SaveChanges();

                        return Result.SuccessFormat("Product has been deleted from basket.");
                    }

                    basketItem.Quantity = basketItem.Quantity - quantity;                    
                    context.SaveChanges();

                    return Result.SuccessFormat("Product has been decreased from basket.");

                }

            }
            catch (Exception exception)
            {
                return Result.FatalFormat("BasketService.RemoveProduct Exception: {0}", exception.Message);
            }
        }


    }
}
