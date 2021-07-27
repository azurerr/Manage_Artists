using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Models
{
    public class ArtistAddViewModel
    {
        public ArtistAddViewModel()
        {
            BirthOrStartDate = new DateTime();
            //Albums = new List<AlbumBaseViewModel>();
        }

        [Required]
        [StringLength(150)]
        [Display(Name = "Artist Name or Stage Name")]
        public string Name { get; set; }

        [Display(Name = "If applicable, Artist's Birth Name")]
        public string BirthName { get; set; }

        [Required]
        [Display(Name = "Birth date or Start date")]
        /*[DisplayFormat(DataFormatString = "{​0:yyyy-MM-dd}​", ApplyFormatInEditMode = true)]*/
        [DataType(DataType.Date)]
        public DateTime BirthOrStartDate { get; set; }

        [Display(Name = "Artist's Primary Genre")]
        public string Genre { get; set; }

        [Required, Display(Name = "Artist Photo")]
        public string UrlArtist { get; set; }

        [Display(Name = "Executive who looks after this artist")]
        public string Executive { get; set; }

        //public IEnumerable<AlbumBaseViewModel> Albums { get; set; }
    }
}