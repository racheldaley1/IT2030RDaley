using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EventApplication.Models
{
    public class Event
    {
        public virtual int EventId { get; set; }
        public virtual int EventTypeId { get; set; }
        public virtual EventType EventType { get; set; }

        [Required (ErrorMessage = "A title is required")]
        [StringLength(50, ErrorMessage = "Title cannot exceed 50 characters")]
        public virtual string Title { get; set; }

        [StringLength(150, ErrorMessage = "The description cannot be more than 150 characters")]
        public virtual string Description { get; set; }

        [Required (ErrorMessage ="A Start Date is required")]
        [Display(Name = "Start Date")]
        public virtual DateTime StartDate { get; set; }

        [Required(ErrorMessage = "An End Date is required")]
        [Display(Name = "End Date")]
        public virtual DateTime EndDate { get; set; }

        [Required(ErrorMessage = "A Start Time is required")]
        [Display(Name = "Start Time")]
        public virtual string StartTime { get; set; }

        [Required(ErrorMessage = "An End Time is required")]
        [Display(Name = "End Time")]
        public virtual string EndTime { get; set; }

        [Required(ErrorMessage = "A maximum amount of tickets is required")]
        [Display(Name = "Max Tickets")]
        public virtual int MaxTickets { get; set; }

        [Required(ErrorMessage = "An available amount of tickets is required")]
        [Display(Name = "Available Tickets")]
        public virtual int AvailableTickets { get; set; }

        [Required(ErrorMessage = "An orgnazier name is required")]
        [Display(Name = "Organizer Name")]
        public virtual string OrganizerName { get; set; }

        [Display(Name = "Organizer Contact Info")]
        public virtual string OrganizerContactInfo { get; set; }

        [Required(ErrorMessage = "A City is required")]
        [Display(Name = "City")]
        public virtual string LocationCity { get; set; }

        [Required(ErrorMessage = "A State is required")]
        [Display(Name = "State")]
        public virtual string LocationState { get; set; }

    }
}