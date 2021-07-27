using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LoadDataController : Controller
    {
        // Reference to the manager object
        Manager m = new Manager();

        // GET: LoadData
        //[AllowAnonymous]
        public ActionResult Index()
        {
            if (m.LoadData())
            {
                return Content("data has been loaded");
            }
            else
            {
                return Content("data exists already");
            }
        }

        // To load all data related to albums at once
        // GET: LoadData/All
        public ActionResult All()
        {

            if (m.LoadDataGenre())
            {
                ViewBag.Result = "Genre data was loaded\n";
            }
            else
            {
                ViewBag.Result = "(Genre done)\n";
            }

            if (m.LoadDataArtist())
            {
                ViewBag.Result += "Artist data was loaded\n";
            }
            else
            {
                ViewBag.Result += "(Artist done)\n";
            }

            if (m.LoadDataAlbum())
            {
                ViewBag.Result += "Album data was loaded\n";
            }
            else
            {
                ViewBag.Result += "(Album done)\n";
            }

            if (m.LoadDataTrack())
            {
                ViewBag.Result += "Track data was loaded\n";
            }
            else
            {
                ViewBag.Result += "(Track done)\n";
            }
            
            return View("result");
            
        }

        public ActionResult Remove()
        {
            if (m.RemoveData())
            {
                return Content("data has been removed");
            }
            else
            {
                return Content("could not remove data");
            }
        }

        public ActionResult RemoveDatabase()
        {
            if (m.RemoveDatabase())
            {
                return Content("database has been removed");
            }
            else
            {
                return Content("could not remove database");
            }
        }

    }
}