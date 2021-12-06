using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Floward.OnlineStore.Core.Models
{
    public class User : IdentityUser
    {
        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
