using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Models
{
    public class TrackAddFormViewModel: TrackAddViewModel
    {

        //public string Genre { get; set; }

        [Display(Name = "Track Genre")]
        public SelectList GenreList { get; set; }

        // To be displayed only
        public string AlbumName { get; set; }



    }
}