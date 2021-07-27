using Assignment5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    [Authorize]
    public class AlbumsController : Controller
    {
        private Manager m = new Manager();

        // GET: Albums
        public ActionResult Index()
        {
            return View(m.AlbumGetAll());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            var item = m.AlbumGetById(id.GetValueOrDefault());

            if (item == null) { return null; }

            return View(item);
        }


        [Authorize(Roles = "Clerk")]
        [Route("album/{id}/addtrack")]
        public ActionResult AddTrack(int? id)
        {
            var album = m.AlbumGetById(id.GetValueOrDefault());
            if (album == null) { return null; }

            var form = new TrackAddFormViewModel();
            form.AlbumName = album.Name;
            form.AlbumId = album.Id;
            form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");

            return View(form);
        }

        [Route("album/{id}/addtrack")]
        [HttpPost]
        public ActionResult Create(TrackAddViewModel newItem)
        {
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }
            var addedItem = m.TrackAdd(newItem);
            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Details", "Tracks", new { id = addedItem.Id });
            }

        }



        // GET: Albums/Create
        /*public ActionResult Create()
        {
            var form = new AlbumAddFormViewModel();
            form.GenreList = new SelectList(m.GenreGetAll(), "Id", "Name");
            form.ArtistList = new SelectList(m.ArtistGetAll(), "Id", "Name");
            form.TrackList = new SelectList(m.TrackGetAll(), "Id", "Name");
            return View(form);
        }

        // POST: Albums/Create
        [HttpPost]
        public ActionResult Create(AlbumAddViewModel newItem)
        {
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }
            var addedItem = m.AlbumAdd(newItem);
            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", new { id = addedItem.Id });
            }

        }*/


    }
}
