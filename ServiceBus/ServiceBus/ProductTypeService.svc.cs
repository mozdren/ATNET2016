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
        /// 
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        public EntityModels.ProductType GetProductType(ProductTypes productType)
        {
            try
            {
                using (var context = new EntityModels.ServiceBusDatabaseEntities())
                {
                    var type = context.ProductTypes.FirstOrDefault(p => p.Type == productType.ToString());

                    if (type != null)
                    {
                        return type;
                    }

                    return new EntityModels.ProductType() {Id = Guid.Empty, Campaign = null, Product = null, Type = "UnknownProductType"};
                }

            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to get product type by its ID
        /// </summary>
        /// <param name="guid">Product type identifier</param>
        /// <returns>Product Type DTO</returns>
        public SharedLibs.DataContracts.ProductType GetProdcutType(Guid guid)
        {
            try
            {
                using (var context = new EntityModels.ServiceBusDatabaseEntities())
                {
                    var productType = context.ProductTypes.FirstOrDefault(pt => pt.Id == guid);

                    if (productType == null)
                    {
                        return new SharedLibs.DataContracts.ProductType()
                        {
                            Result = SharedLibs.DataContracts.Result.ErrorFormat("Product type {0} not found.", guid)
                        };
                    }

                    
                    var queryCampaigns = from c in context.Campaigns
                        where c.ProductType.Id == guid
                        select new SharedLibs.DataContracts.Campaign()
                        {   //Obsolote DTO
                            Discount = c.Discount.Value,
                            Name = c.Name,
                            Id = c.Id
                        };

                    var queryProducts = from p in context.Products
                        where p.ProductType.Id == guid
                        select new SharedLibs.DataContracts.Product()
                        {
                            ID = p.Id,
                            Name = p.Name,
                            //Result = Result.Success("Yay !")
                        };
                    

                    return new SharedLibs.DataContracts.ProductType()
                    {
                        Result = SharedLibs.DataContracts.Result.SuccessFormat("Product type {0} found", guid),
                        ID = guid,
                        Campaings = new SharedLibs.DataContracts.Campaigns() { Items = queryCampaigns.ToList(), Result = Result.Success("Yay !")},
                        Products = new SharedLibs.DataContracts.Products() { Items = queryProducts.ToList(), Result = Result.Success("Yay !")}
                    };
                }
            }
            catch (Exception exception)
            {
                return new SharedLibs.DataContracts.ProductType()
                {
                    Result = SharedLibs.DataContracts.Result.FatalFormat("ProductTypeService.GetProductType Exception: {0}", exception.Message)
                };
            }
        }

    }
}
