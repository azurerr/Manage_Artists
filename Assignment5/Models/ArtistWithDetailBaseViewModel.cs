using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5.Models
{
    public class ArtistWithDetailBaseViewModel: ArtistBaseViewModel
    {
        public ArtistWithDetailBaseViewModel()
        {
            Albums = new List<AlbumBaseViewModel>();
        }

        [Display(Name = "Number of Albums")]
        public int AlbumsCount { get; set; }


        public IEnumerable<AlbumBaseViewModel> Albums { get; set; }
    }
}