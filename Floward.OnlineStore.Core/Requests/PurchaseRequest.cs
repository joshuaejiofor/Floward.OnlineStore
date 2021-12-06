using Floward.OnlineStore.Core.Models;
using System;

namespace Floward.OnlineStore.Core.Requests
{
    public class PurchaseRequest : ICloneable
    {
        public ProductType ProductType { get; set; }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
