using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventApplication.Models;

namespace EventApplication.Controllers
{
    [Authorize]
    public class TicketsOrderedController : Controller
    {
        EventDB db = new EventDB();

        // GET: TicketsOrdered
        public ActionResult Index()
        {
            TicketsOrdered order = TicketsOrdered.GetOrder(this.HttpContext);

            TicketsOrderedViewModel vm = new TicketsOrderedViewModel()
            {
                OrderItems = order.GetOrderItems(),
            };
            return View(vm);
        }

        public ActionResult AddToOrder(int id)
        {
            TicketsOrdered order = TicketsOrdered.GetOrder(this.HttpContext);
            order.AddToOrder(id);
            return RedirectToAction("Index");
        }

        //POST: TicketsOrdered/RemoveFromOrder
        [HttpPost]
        public ActionResult RemoveFromOrder(int id)
        {
            TicketsOrdered cart = TicketsOrdered.GetOrder(this.HttpContext);

            Event events = db.Orders.SingleOrDefault(c => c.RecordId == id).EventSelected;

            int newItemCount = cart.RemoveFromOrder(id);

            TicketsOrderedRemoveViewModel vm = new TicketsOrderedRemoveViewModel()
            {
                DeleteId = id,
                ItemCount = newItemCount,
                Message = events.Title + " has been removed from the order"
            };

            return Json(vm);
        }
    }

}
