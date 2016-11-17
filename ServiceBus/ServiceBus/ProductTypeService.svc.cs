using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SharedLibs.DataContracts;

using SharedLibs.Enums;

namespace ServiceBus
{
	public class ProductTypeService : IProductTypeService
	{
        /// <summary>
        /// Adds new product type
        /// </summary>
        /// <param name="typeName">Name for a new type</param>
        /// <returns></returns>
	    public Result AddProductType(string typeName)
	    {
            try
            {
                using (var context = new ServiceBusDatabaseEntities())
                {
                    if (!string.IsNullOrWhiteSpace(typeName) && typeName.Length <= 50)
                    {
                        context.ProductTypes.Add(new ProductType() { Type = typeName });
                        context.SaveChanges();

                        return Result.Success("New product type has been added.");
                    }

                    return Result.Error("Product type was not added. Please check product type name.");
                }
            }
            catch (Exception execption)
            {
                return Result.FatalFormat("ProductTypeService.AddProductType {0}", execption.Message);
            }
	    }

        /// <summary>
        /// Gets product type by its ID
        /// </summary>
        /// <param name="productTypeE">Identifier of a product</param>
        /// <returns></returns>
	    public SharedLibs.DataContracts.ProductType GetProductType(ProductTypes productTypeE)
	    {
	        try
	        {
	            using (var context = new ServiceBusDatabaseEntities())
	            {   //I don't know how else use enums. As long as DB is "set up" as is now it will work. Otherwise I strongly doubt.
	                var productType = context.ProductTypes.FirstOrDefault(pt => pt.Id == (int) productTypeE);
                                            
	                if (productType != null)
	                {
	                    var queryProductTypeProducts = from contextItem in productType.Product
	                        select new SharedLibs.DataContracts.Product()
	                        {
	                            ID = contextItem.Id,
	                            Name = contextItem.Name,
	                            Price = contextItem.Price.HasValue ? contextItem.Price.Value : 0,
                                Result = Result.Success()
	                            //TODO: I'll leave it like this since I do not know whether I do have to return all the stuff.
	                        };
	                    var listOfProductTypeProducts = queryProductTypeProducts.ToList();

	                    var queryProductTypeCampaigns = from contextItem in productType.Campaign
	                        select new SharedLibs.DataContracts.Campaign()
	                        {
	                            Discount = contextItem.Discount.HasValue ? contextItem.Discount.Value : 0,
	                            //TODO: FIX. Id = contextItem.Id,
	                            Name = contextItem.Name,
	                            Result = Result.Success()

	                        };
	                    var listOfProductTypeCampaigns = queryProductTypeCampaigns.ToList();

	                    return new SharedLibs.DataContracts.ProductType()
	                    {
	                        //TODO: Fix. ID = productType.Id
	                        Type = productType.Type,
	                        Campaings = listOfProductTypeCampaigns,
	                        Products = listOfProductTypeProducts
	                    };
	                }
	                return new SharedLibs.DataContracts.ProductType()
	                {
	                    Result = Result.ErrorFormat("Product type with ID {0} was not found", productTypeE)
	                };
	            }
	        }
	        catch (Exception exception)
	        {
	            return new SharedLibs.DataContracts.ProductType()
	            {
	                Result = Result.FatalFormat("ProductTypeService.GetProductType error: {0}", exception.Message)
	            };
	        }
	    }
	}
}
