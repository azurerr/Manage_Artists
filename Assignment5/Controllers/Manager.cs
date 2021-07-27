// **************************************************
// WEB524 Project Template V2 == 399b256a-5aaf-45a1-88dc-5693962f6976
// Do not change this header.
// **************************************************

using Assignment5.EntityModels;
using Assignment5.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Assignment5.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();
                cfg.CreateMap<Genre, GenreBaseViewModel>();
                cfg.CreateMap<AlbumAddViewModel, Album>();
                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<Album, AlbumWithDetailBaseViewModel>();
                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<Artist, ArtistWithDetailBaseViewModel>();
                cfg.CreateMap<ArtistAddViewModel, Artist>();
                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackWithDetailBaseViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>();


            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()


        public IEnumerable<GenreBaseViewModel> GenreGetAll()
        {
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBaseViewModel>>(ds.Genres.OrderBy(g => g.Name));
        }

        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(ds.Artists.OrderBy(a => a.Name));
        }

        public ArtistWithDetailBaseViewModel ArtistGetById(int id)
        {
            
            var artist = ds.Artists.Include("Albums").SingleOrDefault(a => a.Id == id);

            return artist == null ? null : mapper.Map<Artist, ArtistWithDetailBaseViewModel>(artist);
        }

        public ArtistWithDetailBaseViewModel ArtistAdd(ArtistAddViewModel newItem)
        {
            var user = HttpContext.Current.User.Identity.Name;

            var addedItem = ds.Artists.Add(mapper.Map<ArtistAddViewModel, Artist>(newItem));

            //TODO
            addedItem.Executive = user;
            ds.SaveChanges();
            return addedItem == null ? null : mapper.Map<Artist, ArtistWithDetailBaseViewModel>(addedItem);

        }

        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            var albums = ds.Albums.Include("Artists").OrderByDescending(a => a.ReleaseDate);
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(albums);
        }

        public AlbumWithDetailBaseViewModel AlbumGetById(int id)
        {
            //var album = ds.Albums.Include("Artists").Include("Tracks").SingleOrDefault(a=>a.Id == id);
            //return mapper.Map<AlbumWithDetailBaseViewModel>(album);

            var album = ds.Albums.Include("Artists").Include("Tracks").SingleOrDefault(t => t.Id == id);
            if (album == null)
            {
                return null;
            }
            else
            {
                var result = mapper.Map<Album, AlbumWithDetailBaseViewModel>(album);
                result.ArtistNames = album.Artists.Select(a => a.Name);
                return result;
            }

        }

        public AlbumWithDetailBaseViewModel AlbumAdd(AlbumAddViewModel newItem)
        {

            //Validate each of the associated Artists
            var tempArtists = new List<Artist>();
            foreach (var one in newItem.ArtistIds)
            {
                var artist = ds.Artists.Find(one);
                tempArtists.Add(artist);
            }

            //Validate each of the associated tracks
            var tempTracks = new List<Track>();
            foreach(var one in newItem.TrackIds)
            {
                var track = ds.Tracks.Find(one);
                tempTracks.Add(track);
            }

            if (tempArtists.Count() > 0)
            {
                // Attempt to add the new album
                var addedItem = ds.Albums.Add(mapper.Map<AlbumAddViewModel, Album>(newItem));

                addedItem.Artists = tempArtists;
                addedItem.Tracks = tempTracks;

                // Set the Coordinator user name property
                var user = HttpContext.Current.User.Identity.Name;
                addedItem.Coordinator = user;

                ds.SaveChanges();

                return mapper.Map<Album, AlbumWithDetailBaseViewModel>(addedItem);
            }

            else
                return null;
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            //TODO-later you can modify it to return Artist objects that have some associated data
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks.OrderBy(t => t.Name));
        }

        public TrackWithDetailBaseViewModel TrackGetById(int id)
        {
            var track = ds.Tracks.Include("Albums.Artists").SingleOrDefault(t=>t.Id == id);
            if (track == null)
            {
                return null;
            }
            else
            {
                var result = mapper.Map<Track, TrackWithDetailBaseViewModel>(track);
                result.AlbumNames = track.Albums.Select(a => a.Name);
                return result;
            }
        }

        public TrackWithDetailBaseViewModel TrackAdd(TrackAddViewModel newItem)
        {
            var user = HttpContext.Current.User.Identity.Name;
            var addedItem = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newItem));

            var album = ds.Albums.Find(newItem.AlbumId);
            if (album == null) { return null; };

            //TODO
            //addedItem.AlbumId = album;

            addedItem.Clerk = user;
            addedItem.Albums = new List<Album> { album };
            ds.SaveChanges();
            return addedItem == null ? null : mapper.Map<Track, TrackWithDetailBaseViewModel>(addedItem);
        }


        public IEnumerable<TrackBaseViewModel> TrackGetAllByArtistId(int id)
        {
            var artists = ds.Artists.Include("Album.Track").SingleOrDefault(a => a.Id == id);
            if (artists == null)
            {
                return null;
            }
            var tracks = new List<Track>();
            foreach (var album in artists.Albums)
            {
                tracks.AddRange(album.Tracks);
            }
            tracks = tracks.Distinct().ToList();
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(tracks.OrderBy(t => t.Name));
        }


        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadDataGenre()
        {
            if (ds.Genres.Count() > 0) { return false; }

            ds.Genres.Add(new Genre { Name = "Rock" });
            ds.Genres.Add(new Genre { Name = "Pop" });
            ds.Genres.Add(new Genre { Name = "Funk" });
            ds.Genres.Add(new Genre { Name = "Electronic" });
            ds.Genres.Add(new Genre { Name = "Reggae" });
            ds.Genres.Add(new Genre { Name = "Soul" });
            ds.Genres.Add(new Genre { Name = "Country" });
            ds.Genres.Add(new Genre { Name = "Latin" });
            ds.Genres.Add(new Genre { Name = "Kpop" });
            ds.Genres.Add(new Genre { Name = "Classical" });

            ds.SaveChanges();
            return true;
        }

        public bool LoadDataArtist()
        {
            if (ds.Artists.Count() > 0) { return false; }

            var user = HttpContext.Current.User.Identity.Name;

            ds.Artists.Add(new Artist
            {
                Name = "Ariana Grande",
                BirthName = "Ariana Grande-Butera",
                UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/dd/Ariana_Grande_Grammys_Red_Carpet_2020.png/375px-Ariana_Grande_Grammys_Red_Carpet_2020.png",
                BirthOrStartDate = new DateTime(1993, 6, 26),
                Genre = "Pop",
                Executive = user
            });
            ds.Artists.Add(new Artist
            {
                Name = "Grupo Extra",
                UrlArtist = "http://www.grupoextraofficial.com/uploads/7/8/9/0/78905998/banner-promo-the-movie_orig.jpg",
                BirthOrStartDate = new DateTime(2010, 11, 02),
                Genre = "Latin",
                Executive = user
            });
            ds.Artists.Add(new Artist
            {
                Name = "BTS",
                UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/50/BTS_on_the_red_carpet_of_Korean_Popular_Culture_%26_Arts_Awards_on_October_24%2C_2018_%283%29.png/450px-BTS_on_the_red_carpet_of_Korean_Popular_Culture_%26_Arts_Awards_on_October_24%2C_2018_%283%29.png",
                BirthOrStartDate = new DateTime(2013, 06, 13),
                Genre = "Kpop",
                Executive = user
            });

            ds.SaveChanges();
            return true;
        }

        public bool LoadDataAlbum()
        {
            if (ds.Albums.Count() > 0) { return false; }

            var user = HttpContext.Current.User.Identity.Name;

            var ariana = ds.Artists.SingleOrDefault(a => a.Name == "Ariana Grande");
            if (ariana == null) { return false; }
            ds.Albums.Add(new Album
            {
                Artists = new List<Artist> { ariana },
                Name = "Thank U, Next",
                UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/d/dd/Thank_U%2C_Next_album_cover.png",
                ReleaseDate = new DateTime(2019, 02, 8),
                Genre = "Pop",
                Coordinator = user
            });
            ds.Albums.Add(new Album
            {
                Artists = new List<Artist> { ariana },
                Name = "Positions",
                UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/a/a0/Ariana_Grande_-_Positions.png",
                ReleaseDate = new DateTime(2020, 10, 30),
                Genre = "Pop",
                Coordinator = user
            });

            var grupo = ds.Artists.SingleOrDefault(a => a.Name == "Grupo Extra");
            if (grupo == null) { return false; }
            ds.Albums.Add(new Album
            {
                Artists = new List<Artist> { grupo },
                Name = "The Movie",
                UrlAlbum = "http://www.grupoextraofficial.com/uploads/7/8/9/0/78905998/published/contraportada-the-movie.jpg?1516929569",
                ReleaseDate = new DateTime(2018, 05, 05),
                Genre = "Latin",
                Coordinator = user
            });
            ds.Albums.Add(new Album
            {
                Artists = new List<Artist> { grupo },
                Name = "Colores",
                UrlAlbum = "https://i.scdn.co/image/ab67616d0000b273262b14c2ec6eb11849c7e926",
                ReleaseDate = new DateTime(2017, 01, 12),
                Genre = "Latin",
                Coordinator = user
            });

            var bts = ds.Artists.SingleOrDefault(a => a.Name == "BTS");
            if (bts == null) { return false; }
            ds.Albums.Add(new Album
            {
                Artists = new List<Artist> { bts },
                Name = "MAP OF THE SOUL",
                UrlAlbum = "https://upload.wikimedia.org/wikipedia/ko/3/38/%EB%B0%A9%ED%83%84%EC%86%8C%EB%85%84%EB%8B%A8_-_MAP_OF_THE_SOUL_-_7.jpg",
                ReleaseDate = new DateTime(2020, 02, 21),
                Genre = "Kpop",
                Coordinator = user
            });
            ds.Albums.Add(new Album
            {
                Artists = new List<Artist> { bts },
                Name = "LOVE YOURSELF 轉 Tear",
                UrlAlbum = "https://upload.wikimedia.org/wikipedia/ko/thumb/7/77/%EB%B0%A9%ED%83%84%EC%86%8C%EB%85%84%EB%8B%A8_LOVE_YOURSELF_%E8%BD%89_%27Tear%27.jpeg/220px-%EB%B0%A9%ED%83%84%EC%86%8C%EB%85%84%EB%8B%A8_LOVE_YOURSELF_%E8%BD%89_%27Tear%27.jpeg",
                ReleaseDate = new DateTime(2018, 05, 18),
                Genre = "Kpop",
                Coordinator = user
            });
            ds.SaveChanges();
            return true;
        }

        public bool LoadDataTrack()
        {
            if (ds.Tracks.Count() > 0) { return false; }

            var user = HttpContext.Current.User.Identity.Name;

            var thanks = ds.Albums.SingleOrDefault(a => a.Name == "Thank U, Next");
            if (thanks == null) { return false; }
            ds.Tracks.Add(new Track
            { Albums = new List<Album> { thanks }, Name = "Imagine", Composeres = "Ariana Grande", Genre = "Pop", Clerk = user });
            ds.Tracks.Add(new Track
            { Albums = new List<Album> { thanks }, Name = "Needy", Composeres = "Victoria Monét", Genre = "Rock", Clerk = user });
            ds.Tracks.Add(new Track
            { Albums = new List<Album> { thanks }, Name = "NASA", Composeres = "Tayla Parx", Genre = "Soul", Clerk = user });
            ds.Tracks.Add(new Track
            { Albums = new List<Album> { thanks }, Name = "Bloodline", Composeres = "Max Martin", Genre = "Pop", Clerk = user });
            ds.Tracks.Add(new Track
            { Albums = new List<Album> { thanks }, Name = "Thank U, Next", Composeres = "Ariana Grande", Genre = "Pop", Clerk = user });

            var positions = ds.Albums.SingleOrDefault(a => a.Name == "Positions");
            if (thanks == null) { return false; }
            ds.Tracks.Add(new Track
            { Albums = new List<Album> { positions }, Name = "Shut Up", Composeres = "Ariana Grande", Genre = "Pop", Clerk = user });
            ds.Tracks.Add(new Track
            { Albums = new List<Album> { positions }, Name = "34+35", Composeres = "Scott Nicholson", Genre = "R&B", Clerk = user });
            ds.Tracks.Add(new Track
            { Albums = new List<Album> { positions }, Name = "Motive", Composeres = "Amala Dlamini", Genre = "Soul", Clerk = user });
            ds.Tracks.Add(new Track
            { Albums = new List<Album> { positions }, Name = "Just like Magic", Composeres = "Shea Taylor", Genre = "Pop", Clerk = user });
            ds.Tracks.Add(new Track
            { Albums = new List<Album> { positions }, Name = "Positions", Composeres = "Ariana Grande", Genre = "Pop", Clerk = user });

            ds.SaveChanges();
            return true;
        }

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Role claims

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims here

                ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Clerk" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Staff" });
                //ds.RoleClaims.Add(new RoleClaim { Name = "Admin" });
                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }

                foreach (var e in ds.Artists)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                foreach (var e in ds.Albums)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                foreach (var e in ds.Tracks)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "RequestUser" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it

    // How to use...

    // In the Manager class, declare a new property named User
    //public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value
    //User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

}