using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObject
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int? MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }
        [JsonIgnore] public virtual Member? Member { get; set; }
        [JsonIgnore] public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}