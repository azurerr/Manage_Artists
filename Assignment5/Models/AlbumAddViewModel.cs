using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Models
{
    public class AlbumAddViewModel
    {

        public AlbumAddViewModel()
        {
            ArtistIds = new List<int>();
            TrackIds = new List<int>();
            ReleaseDate = DateTime.Now;
        }

        [Required, StringLength(150), Display(Name = "Album Name")]
        public string Name { get; set; }

        [Required, Display(Name = "Release Date")]
        /*[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]*/
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Artist's Primary Genre")]
        public string Genre { get; set; }

        [Required, Display(Name = "URL to Album Image(Cover art)")]
        public string UrlAlbum { get; set; }

        /*[StringLength(150), Display(Name = "Coordinator who looks after the Album")]
        public string Coordinator { get; set; }*/


        [Required]
        public IEnumerable<int> ArtistIds { get; set; }

        [Required]
        public IEnumerable<int> TrackIds { get; set; }

    }
}