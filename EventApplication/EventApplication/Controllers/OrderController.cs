﻿using System;
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
            Order events = db.Orders.Find(id);
            return View(events);

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