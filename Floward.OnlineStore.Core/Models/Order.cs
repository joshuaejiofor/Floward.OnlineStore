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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (Order)obj;

            return Id == other.Id &&
                UserId == other.UserId &&
                ProductId == other.ProductId &&
                OrderStatus == other.OrderStatus;
        }
                

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ UserId.GetHashCode();
        }


    }
}
