using System;
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
        public Product GetProduct(Guid guid)
        {
            try
            {
                // not yet implemented, therefore, we throw this exception
                throw new NotImplementedException();
            }
            catch (Exception exception)
            {
                return new Product { Result = Result.Fatal(string.Format("ProductService.GetProduct Exception: {0}", exception.Message)) };
            }
        }
    }
}
