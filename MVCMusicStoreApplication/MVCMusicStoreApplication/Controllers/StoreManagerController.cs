using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCMusicStoreApplication.Models;

namespace MVCMusicStoreApplication.Controllers
{
    public class StoreManagerController : Controller
    {
        private MVCMusicStoreDB db = new MVCMusicStoreDB();

        public ActionResult DailyDeal()
        {
            var album = GetDailyDeal();
            return PartialView("_DailyDeal", album);
        }
        //Select an album and discount it by 50%
        private Album GetDailyDeal()
        {
            var album = db.Albums
                .OrderBy(a => System.Guid.NewGuid())
                .First();

            //Apply 50% discount
            album.Price *= 0.5m;
            return album;
        }

        public ActionResult ArtistSearch(string q)
        {
            var artists = GetArtists(q);
            return PartialView("_ArtistSearch", artists);
        }

        private List<Artist> GetArtists(string searchString)
        {
            return db.Artists
                .Where(a => a.Name.Contains(searchString))
                .ToList();
        }
        // GET: StoreManager
        public ActionResult Index()
        {
            var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre);
            return View(albums.ToList());
        }

        // GET: StoreManager/Details/5
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

        // GET: StoreManager/Create
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name");
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
            return View();
        }

        // POST: StoreManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // GET: StoreManager/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // POST: StoreManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // GET: StoreManager/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Test ()
        {
            var albums = db.Albums;

            //Finds albums by Album name

            // var albumsByName = (from a in albums
            //                   where a.Title == "Stormbringer"
            //                   select a);

            var albumsByName = albums.Where(a => a.Title == "Stormbringer");

            //Finds ablums by Artist Name

            //var albumsByArtist = (from a in albums
            //                      where a.Artist.Name == "Chic"
            //                     select a);
            var albumsByArtist = albums.Where(a => a.Artist.Name == "Chic");
            //Finds albums by Genre

            //var albumsByGenre = (from a in albums
            //                     where a.Genre.Name == "Classical"
            //                     orderby a.Title descending
            //                     select a);
            var albumsByGenre = albums.Where(a => a.Genre.Name == "Classical").OrderByDescending(a => a.Title);

            ViewBag.AlbumsByName = new SelectList(albumsByName, "AlbumId", "Title", albumsByName.First().AlbumId);
            ViewBag.AlbumsByArtist = new SelectList(albumsByArtist, "AlbumId", "Title", albumsByArtist.First().AlbumId);
            ViewBag.AlbumsByGenre = new SelectList(albumsByGenre, "AlbumId", "Title", albumsByGenre.First().AlbumId);
            return View();
        }
    }
}
