﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment5.Models
{
    public class ArtistAddFormViewModel: ArtistAddViewModel
    {

        [Display(Name = "Artist's Primary Genre")]
        public SelectList GenreList { get; set; }


    }
}