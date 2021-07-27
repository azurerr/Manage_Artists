using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Models
{
    public class AlbumAddFormViewModel: AlbumAddViewModel
    {

        [Display(Name = "Album's Primary Genre")]
        public SelectList GenreList { get; set; }

        //Display only
        public string ArtistName { get; set; }

        public MultiSelectList ArtistList { get; set; }

        public MultiSelectList TrackList { get; set; }



    }
}