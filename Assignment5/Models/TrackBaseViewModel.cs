using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Models
{
    public class TrackBaseViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Track Name")]
        public string Name { get; set; }

        [Display(Name = "Composer name(s)")]
        public string Composeres { get; set; }

        [Display(Name = "Track Genre")]
        public string Genre { get; set; }

        [Display(Name = "Clerk who helps with Album Tasks")]
        public string Clerk { get; set; }

    }
}