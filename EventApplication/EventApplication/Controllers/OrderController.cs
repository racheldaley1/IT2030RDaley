using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventApplication.Models;

namespace EventApplication.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private EventDB db = new EventDB();
        [HttpGet]
        public ActionResult Details(int id)
        {
            Event selectedEvent = this.db.Events.Find(id);

            Order order = new Order
            {
                EventId = id,
                EventSelected = selectedEvent
            };

            return this.View(order);

            //Event events = db.Events.Find(id);
            //return View(events);
        }


        public ActionResult OrderSummary(int id)
        {
            Event selectedEvent = this.db.Events.Find(id);

            Order order = new Order
            {
                EventId = id,
                EventSelected = selectedEvent,
            };

            return this.View(order);
        }
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
    }
}