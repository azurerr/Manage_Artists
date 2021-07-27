using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assignment5.EntityModels
{
    [Table("Track")]
    public class Track
    {
        public Track()
        {
            Albums = new HashSet<Album>();
        }
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Name { get; set; }

        // holds the username of the authenticated user 
        // who is in the process of adding a new Track object
        public string Clerk { get; set; }

        // The user will simply type comma separators 
        //between the names of multiple composers
        public string Composeres { get; set; }

        public string Genre { get; set; }

        public ICollection<Album> Albums { get; set; }
    }
}