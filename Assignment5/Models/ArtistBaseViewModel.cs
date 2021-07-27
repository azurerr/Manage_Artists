using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5.Models
{
    public class ArtistBaseViewModel
    {
        public ArtistBaseViewModel()
        {
            BirthOrStartDate = new DateTime(2000, 01, 01);
        }

        public int Id { get; set; }

        [Required, StringLength(150)]
        [Display(Name = "Artist Name or Stage Name")]
        public string Name { get; set; }

        // only for a person who uses a stage name
        [Display(Name = "If applicable, Artist's Birth Name")]
        public string BirthName { get; set; }

        [Display(Name = "Birth date or Start date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime BirthOrStartDate { get; set; }

        [Display(Name = "Artist Photo")]
        public string UrlArtist { get; set; }

        [Display(Name = "Artist's Primary Genre")]
        public string Genre { get; set; }

        // the username (e.g. amanda@example.com) of the authenticated user
        [Display(Name = "Executive who looks after this artist")]
        public string Executive { get; set; }

        //[Display(Name = "Number of Albums")]
        //public int AlbumsNum { get; set; }



    }
}