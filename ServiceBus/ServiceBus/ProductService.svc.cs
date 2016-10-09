using System;
using System.Linq;

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
                            Result = SharedLibs.DataContracts.Result.Error("Product not found.")
                        };
                    }

                    return new SharedLibs.DataContracts.Product
                    {
                        Result = SharedLibs.DataContracts.Result.Success("Product found"),
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
                    Result = SharedLibs.DataContracts.Result.Fatal(string.Format("ProductService.GetProduct Exception: {0}", exception.Message))
                };
            }
        }
    }
}
