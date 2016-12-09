using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using ServiceBus.EntityModels;
using SharedLibs.Enums;

namespace ServiceBus
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductTypeService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProductTypeService.svc or ProductTypeService.svc.cs at the Solution Explorer and start debugging.
    public class ProductTypeService : IProductTypeService
    {
        //TODO: Rewrite to return DTO.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        public ProductType GetProductType(ProductTypes productType)
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

                    return new ProductType() {Id = Guid.Empty, Campaign = null, Product = null, Type = "UnknownProductType"};
                }

            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
