using System.Text.Json.Serialization;

namespace Floward.OnlineStore.Core.Models
{
    public class Order : EntityBase
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public OrderStatus OrderStatus { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
        

    }
}
