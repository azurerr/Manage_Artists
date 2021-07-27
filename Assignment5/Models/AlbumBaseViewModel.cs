using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Models
{
    public class AlbumBaseViewModel
    {
        public AlbumBaseViewModel()
        {
            ReleaseDate = new DateTime(2000, 01, 01);
        }

        public int Id { get; set; }

        [Required, StringLength(150), Display(Name = "Album Name")]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Required, Display(Name = "Album Image(Cover art)")]
        public string UrlAlbum { get; set; }

        [Display(Name = "Album's Primary Genre")]
        public string Genre { get; set; }


        [Required, StringLength(150), Display(Name = "Coordinator who looks after the Album")]
        public string Coordinator { get; set; }


    }
}