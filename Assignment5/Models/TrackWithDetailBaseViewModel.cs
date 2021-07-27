using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5.Models
{
    public class TrackWithDetailBaseViewModel: TrackBaseViewModel
    {
        public TrackWithDetailBaseViewModel()
        {
            AlbumNames = new List<string>(); 
            Albums = new List<AlbumBaseViewModel>();
        }
        [Display(Name = "Number of Albums with this Track")]
        public int AlbumsCount { get; set; }

        [Display(Name = "Albums with this Track")]
        public IEnumerable<string> AlbumNames { get; set; }

        public IEnumerable<AlbumBaseViewModel> Albums { get; set; }

    }
}