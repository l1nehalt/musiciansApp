using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusiciansApp.Model;
using MusiciansApp.Repository;

namespace MusiciansApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly MusicDbContext _context;

        public HomeController(MusicDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search(string query)
        {
            query = query.ToLower();

            var artist = _context.Artists
                         .FirstOrDefault(a => a.Name.Contains(query));

            var song = _context.Songs
                        .FirstOrDefault(a => a.Title.Contains(query));

            var album = _context.Albums
                        .FirstOrDefault(a => a.Title.Contains(query));

            if (artist != null)
            {
                return RedirectToAction("ArtistDetails", new { artistId = artist.Id });
            }

            if (song != null)
            {
                return RedirectToAction("SongDetails", new { songId = song.Id });
            }

            if (album != null)
            {
                return RedirectToAction("AlbumDetails", new { albumId = album.Id });
            }

            ViewBag.Message = "Исполнитель, альбом или трек не найдены.";
            return View("Index");
        }

        [HttpGet]
        public IActionResult ArtistDetails(int artistId)
        {
            var artist = _context.Artists
                .Include(a => a.Albums)
                .ThenInclude(a => a.Songs)
                .Include(a => a.Singles)
                .FirstOrDefault(a => a.Id == artistId);

            if (artist == null)
            {
                return NotFound();
            }

            artist.Singles = _context.Songs
                .Where(s => s.ArtistId == artistId && (s.AlbumId == null || s.IsSingle))
                .ToList();

            return View(artist);
        }

        [HttpGet]
        public IActionResult SongDetails(int songId)
        {
            var song = _context.Songs
                        .Include(s => s.Artist)
                        .ThenInclude(s => s.Albums)
                        .FirstOrDefault(s => s.Id == songId);

            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        [HttpGet]
        public IActionResult AlbumDetails(int albumId)
        {
            var album = _context.Albums
                        .Include(s => s.Artist)
                        .Include(s => s.Songs)
                        .FirstOrDefault(s => s.Id == albumId);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }
    }
}
