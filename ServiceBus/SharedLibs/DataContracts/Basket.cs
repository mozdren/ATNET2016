using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibs.DataContracts
{
    [DataContract]
    public class Basket : DTO
    {
        /// <summary>
        /// Identifier
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }
    }
}
