using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventApplication.Models;

namespace EventApplication.Controllers
{
    public class OrderController : Controller
    {
        private EventDB db = new EventDB();
        [HttpGet]
        public ActionResult Details(int id)
        {
            var events = db.Events.Where(a => a.EventId.Equals(id)).ToList();

            return View(events.ToList());

            //Event events = db.Events.Find(id);
            //return View(events);
        }


        public ActionResult OrderSummary(int id)
        {
            Order orders = db.Orders.Find(id);
            return View(orders);
        }
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
    }
}