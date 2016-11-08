using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceBus
{
   
    [ServiceContract]
    public interface IProductTypeService
    {
        [OperationContract]
        SharedLibs.DataContracts.Result AddProductType(string typeName);

        [OperationContract]
        SharedLibs.DataContracts.ProductType GetProductType(int productTypeId);
    }
}
