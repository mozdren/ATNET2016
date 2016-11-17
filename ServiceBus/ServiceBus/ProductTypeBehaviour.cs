using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceBus
{
    public partial class ProductType
    {
        public static ProductType GetProductType(int id)
        {
            try
            {
                using (var context = new ServiceBusDatabaseEntities())
                {
                    var type = context.ProductTypes.FirstOrDefault(t => t.Id == id);

                    if (type != null)
                    {
                        return type;
                    }

                    return null;
                }
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }
}