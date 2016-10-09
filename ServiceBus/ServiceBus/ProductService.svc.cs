using System;
using System.Collections.Generic;
using System.Linq;
using SharedLibs.DataContracts;

namespace ServiceBus
{
    public class ProductService : IProductService
    {
        /// <summary>
        /// This method should return a product with requested guid
        /// </summary>
        /// <param name="guid">Guid of a requested product</param>
        /// <returns>requested product</returns>
        public SharedLibs.DataContracts.Product GetProduct(Guid guid)
        {
            try
            {
                using (var context = new ServiceBusDatabaseEntities())
                {
                    var product = context.Products.FirstOrDefault(p => p.Id == guid);

                    if (product == null)
                    {
                        return new SharedLibs.DataContracts.Product
                        {
                            Result = SharedLibs.DataContracts.Result.ErrorFormat("Product {0} not found.", guid)
                        };
                    }

                    return new SharedLibs.DataContracts.Product
                    {
                        Result = SharedLibs.DataContracts.Result.SuccessFormat("Product {0} found", guid),
                        ID = guid,
                        Name = product.Name,
                        Price = product.Price
                    };
                }
            }
            catch (Exception exception)
            {
                return new SharedLibs.DataContracts.Product
                {
                    Result = SharedLibs.DataContracts.Result.FatalFormat("ProductService.GetProduct Exception: {0}", exception.Message)
                };
            }
        }

        /// <summary>
        /// This method returns all defined products
        /// </summary>
        /// <returns>collection of all products</returns>
        public SharedLibs.DataContracts.Products GetAllProducts()
        {
            try
            {
                using (var context = new ServiceBusDatabaseEntities())
                {
                    var returnCollection = new List<SharedLibs.DataContracts.Product>();

                    foreach (var product in context.Products)
                    {
                        returnCollection.Add(new SharedLibs.DataContracts.Product
                        {
                            ID = product.Id,
                            Name = product.Name,
                            Price = product.Price
                        });
                    }

                    return new SharedLibs.DataContracts.Products
                    {
                        Result = SharedLibs.DataContracts.Result.Success("All products returned"),
                        Items = returnCollection
                    };
                }
            }
            catch (Exception exception)
            {
                return new SharedLibs.DataContracts.Products
                {
                    Result = SharedLibs.DataContracts.Result.FatalFormat("ProductService.GetProduct Exception: {0}", exception.Message)
                };
            }
        }
    }
}
