using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EventApplication.Models
{
    public class Order
    {
        [Key]
        public int RecordId { get; set; }

        public string OrderId { get; set; }
        public int EventId { get; set; }
        public virtual Event EventSelected { get; set; }
        public DateTime DateCreated { get; set; }
        public int TicketCount { get; set; }
        public string OrderStatus { get; set; }
        public int OrderNumber { get; set; }

    }
}