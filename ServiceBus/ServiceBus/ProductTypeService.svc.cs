using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SharedLibs.DataContracts;

namespace ServiceBus
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductTypeService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProductTypeService.svc or ProductTypeService.svc.cs at the Solution Explorer and start debugging.
    public class ProductTypeService : IProductTypeService
    {
        public void DoWork()
        {
        }

        public SharedLibs.DataContracts.Result AddProductType(string typeName)
        {
            if (!String.IsNullOrWhiteSpace(typeName) && typeName.Length >= 1)
            {
                try
                {
                    using (var context = new ServiceBusDatabaseEntities())
                    {
                        if (true) //TODO: Implement GetProductType
                        {
                            context.ProductTypes.Add(new ProductType() { Type = typeName });
                            context.SaveChanges();

                            return Result.SuccessFormat("New type {0} added successfully.", typeName); 
                        }
                    }
                }
                catch (Exception exception)
                {
                    return Result.FatalFormat("ProductTypeService.AddProductType exception: {0}", exception.Message);
                }
            }
            else
            {
                return Result.ErrorFormat("Type {0} could not be added.", typeName);
            }
            
            
        }

        public SharedLibs.DataContracts.ProductType GetProductType(int productTypeId)
        {
            throw new NotImplementedException();
        }
    }
}
