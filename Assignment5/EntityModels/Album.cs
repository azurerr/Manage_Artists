using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assignment5.EntityModels
{
    [Table("Album")]
    public class Album
    {
        public Album()
        {
            Artists = new HashSet<Artist>();
            Tracks = new HashSet<Track>();
            ReleaseDate = DateTime.Now;
        }

        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; }

        // holds the username who is in the process of adding a new Album object
        public string Coordinator { get; set; }

        public string Genre { get; set; }


        public DateTime ReleaseDate { get; set; }

        // hold a URL to an image of the album
        public string UrlAlbum { get; set; }


        public ICollection<Artist> Artists { get; set; }

        public ICollection<Track> Tracks { get; set; }


    }
}