using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Controllers
{
    [Authorize]
    public class TracksController : Controller
    {
        private Manager m = new Manager();
        // GET: Tracks
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        public ActionResult Details(int? id)
        {
            var item = m.TrackGetById(id.GetValueOrDefault());

            if (item == null) { return null; }

            return View(item);
        }







    }
}