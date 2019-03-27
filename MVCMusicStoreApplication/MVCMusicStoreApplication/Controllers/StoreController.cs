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
        public ActionResult Browse()
        {
            return View(db.Genres.ToList());
        }
        // GET: Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }
        // GET: Album
        public ActionResult Index(int id)
        {
            var index = db.Albums
            .Where(a => a.GenreId.Equals(id))
            .ToList();
            return View(index.ToList());
            
        }
    }
}