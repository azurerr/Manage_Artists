using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5.Models
{
    public class AlbumWithDetailBaseViewModel: AlbumBaseViewModel
    {
        public AlbumWithDetailBaseViewModel()
        {
            Artists = new List<ArtistBaseViewModel>();
            Tracks = new List<TrackBaseViewModel>();
            ArtistNames = new List<string>();
        }

        [Display(Name = "Number of Tracks on this album")]
        public int TracksCount { get; set; }

        [Display(Name = "Number of Artists on this album")]
        public int ArtistsCount { get; set; }

        [Display(Name = "Artists with this Albums")]
        public IEnumerable<string> ArtistNames { get; set; }

        public IEnumerable<ArtistBaseViewModel> Artists { get; set; }
        public IEnumerable<TrackBaseViewModel> Tracks { get; set; }
    }
}