using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Floward.OnlineStore.Core.Models
{
    public class Product : EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public ProductType ProductType { get; set; }
        public decimal Discount { get; set; }
        public decimal VAT { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}