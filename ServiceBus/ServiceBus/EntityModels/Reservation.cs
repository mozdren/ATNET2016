//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceBus.EntityModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservation
    {
        public System.Guid Id { get; set; }
        public int Count { get; set; }
        public System.Guid UserId { get; set; }
        public System.Guid StorageId { get; set; }
        public System.Guid ProductId { get; set; }
    
        public virtual User User { get; set; }
        public virtual Storage Storage { get; set; }
        public virtual Product Product { get; set; }
    }
}
