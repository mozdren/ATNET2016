using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceBus
{
    /// <summary>
    /// Basket service for testing purposes
    /// </summary>
    [ServiceContract]
    public interface IBasketService
    {
        /// <summary>
        /// Method is supposed to create new basket. This is a test for database FK implementation.
        /// </summary>
        /// <returns>DTO object basket WITHOUT basket items.</returns>
        [OperationContract]
        SharedLibs.DataContracts.Basket CreateBasekt();
    }
}
