using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assignment5.EntityModels
{
    [Table("Artist")]
    public class Artist
    {

        public Artist()
        {
            BirthOrStartDate = DateTime.Now.AddYears(-20);
            Albums = new HashSet<Album>();
        }

        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; }

        // only for a person who uses a stage name
        public string BirthName { get; set; }

        public DateTime BirthOrStartDate { get; set; }

        // the username (e.g. amanda@example.com) of the authenticated user who completed the process of adding a new Artist object
        // use the security APIs
        public string Executive { get; set; }

        public string Genre { get; set; }

        // hold a URL to a photo of the artist
        public string UrlArtist { get; set; }

        public int AlbumsNum { get; set; }

        public ICollection<Album> Albums { get; set; }


    }
}