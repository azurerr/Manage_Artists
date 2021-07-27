using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Models
{
    public class TrackAddViewModel
    {
        public int Id { get; set; }

        //configure its identifier as a hidden HTML Form element
        public int AlbumId { get; set; }

        [Required, Display(Name = "Track Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Composer name(s)")]
        public string Composeres { get; set; }

        public string Genre { get; set; }


        [Display(Name = "Clerk who helps with Album Tasks")]
        public string Clerk { get; set; }


    }
}