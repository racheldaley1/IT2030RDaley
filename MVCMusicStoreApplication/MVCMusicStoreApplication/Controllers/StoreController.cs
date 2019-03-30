using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using MVCMusicStoreApplication.Models;

namespace MVCMusicStoreApplication.Controllers
{
    public class StoreController : Controller
    {
        private MVCMusicStoreDB db = new MVCMusicStoreDB();

        // GET: Browse
        [HttpGet]
        public ActionResult Browse()
        {
            return View(db.Genres.ToList());
        }
        // GET: Details
        [HttpGet]
        public ActionResult Details(int id)
        {
            Album album = db.Albums.Find(id);
            return View(album);
        }
        // GET: Album
        [HttpGet]
        public ActionResult Index(int id)
        {
            var index = db.Albums
            .Where(a => a.GenreId.Equals(id))
            .ToList();
            return View(index.ToList());
            
        }
    }
}