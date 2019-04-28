using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EventApplication.Models
{
    public class EventType
    {
        public virtual int EventTypeId { get; set; }

        [StringLength(50, ErrorMessage = "Event Type cannot exceed 50 characters")]
        [Display(Name = "Event Type")]
        public virtual string Type { get; set; }
    }
}