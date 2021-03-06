﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventApplication.Models;

namespace EventApplication.Controllers
{
    public class HomeController : Controller
    {
        private EventDB db = new EventDB(); 

        public ActionResult FindEvent(string t, string l)
        {
            List<Event> events = GetEvents(t, l);
            return PartialView("_FindEvent", events);
        }

        private List<Event> GetEvents(string searchstring, string locationsearch)
        {
                return db.Events.Where(a =>

                    (a.Title.Contains(searchstring) || a.EventType.Type.Contains(searchstring))

                    &&

                    (a.LocationCity.Contains(locationsearch) || a.LocationState.Contains(locationsearch))

                     ).OrderBy(b => b.StartDate).ToList();
        }

        public ActionResult LastMinuteDeals()
        {
           List<Event> events = GetLastMinuteDeals();
           return PartialView("_LastMinuteDeals", events);
        }

        private List<Event> GetLastMinuteDeals()
        {
            DateTime now = DateTime.Now;
            DateTime future = DateTime.Now.AddDays(2);
           {
               var events = db.Events.Where(a => a.StartDate <= future && a.StartDate >= now).ToList();
               return events;
            }
            //throw new NotImplementedException();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult FindAnEvent()
        {
            return View();
        }
    }
}