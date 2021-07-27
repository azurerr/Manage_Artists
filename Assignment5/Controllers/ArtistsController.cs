using Assignment5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    [Authorize]
    public class ArtistsController : Controller
    {
        private Manager m = new Manager();

        // GET: Artists
        public ActionResult Index()
        {
            return View(m.ArtistGetAll());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            var item = m.ArtistGetById(id.GetValueOrDefault());

            if (item == null) { return null; }

            return View(item);
        }

        // GET: Artists/Create
        [Authorize(Roles = "Executive")]
        public ActionResult Create()
        {
            var form = new ArtistAddFormViewModel();
            form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");
            return View(form);
        }

        // POST: Artists/Create
        [HttpPost]
        public ActionResult Create(ArtistAddViewModel newItem)
        {
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }
            var addedItem = m.ArtistAdd(newItem);
            if(addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("details", new { id = addedItem.Id });
            }
        }

        [Authorize(Roles = "Coordinator")]
        [Route("artist/{id}/addalbum")]
        public ActionResult AddAlbum(int? id)
        {
            var artist = m.ArtistGetById(id.GetValueOrDefault());
            if (artist == null) { return null; }

            var form = new AlbumAddFormViewModel();
            form.ArtistName = artist.Name;
            form.GenreList = new SelectList(m.GenreGetAll(), "Name", "Name");

            //IEnumerable<string> selectedArtist;

            form.ArtistList = new MultiSelectList
                (items: m.ArtistGetAll(),
                dataValueField: "Id",
                dataTextField: "Name",
                selectedValues: new List<int> { artist.Id } );

            form.TrackList = new MultiSelectList
                (items: m.TrackGetAll(),
                dataValueField: "Id",
                dataTextField: "Name");

            return View(form);

        }

        [Route("artist/{id}/addalbum")]
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
                return RedirectToAction("details", "albums", new { id = addedItem.Id });
            }

        }

        /*        private ActionResult RedirectionToaction()
                {
                    throw new NotImplementedException();
                }

                // GET: Artists/Edit/5
                public ActionResult Edit(int id)
                {
                    return View();
                }

                // POST: Artists/Edit/5
                [HttpPost]
                public ActionResult Edit(int id, FormCollection collection)
                {
                    try
                    {
                        // TODO: Add update logic here

                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        return View();
                    }
                }

                // GET: Artists/Delete/5
                public ActionResult Delete(int id)
                {
                    return View();
                }

                // POST: Artists/Delete/5
                [HttpPost]
                public ActionResult Delete(int id, FormCollection collection)
                {
                    try
                    {
                        // TODO: Add delete logic here

                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        return View();
                    }
                }*/
    }
}
