using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required] public DateTime? OrderDate { get; set; }
        [Required] public DateTime? RequiredDate { get; set; }
        [Required] public DateTime? ShippedDate { get; set; }
        [Required] public decimal? Freight { get; set; }
        public virtual Member? Member { get; set; }
        [JsonIgnore] public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}