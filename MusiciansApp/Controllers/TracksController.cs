using Microsoft.AspNetCore.Mvc;
using MusiciansApp.Model;
using MusiciansApp.Repository;

namespace MusiciansApp.Controllers
{
    public class TracksController : Controller
    {
        private readonly MusicDbContext _context;

        public TracksController(MusicDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AddTrack()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTrack(string title, string musician, IFormFile trackFile)
        {
            if (musician != null && trackFile != null && trackFile.ContentType == "audio/mpeg")
            {
                var fileName = $"{Guid.NewGuid()}_{trackFile.FileName}";
                var filePath = Path.Combine("wwwroot/Uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    trackFile.CopyTo(stream);
                }

                var track = new Track
                {
                    Title = title,
                    Musician = musician,
                    FilePath = $"/Uploads/{fileName}"
                };

                _context.Tracks.Add(track);
                _context.SaveChanges();

                TempData["SuccessMessageTrack"] = "Трек успешно добавлен!";
                return RedirectToAction("Index", "Artists");
            }

            TempData["ErrorMessageTrack"] = "Загрузите MP3 файл.";
            return View();
        }

        [HttpGet]
        public IActionResult SearchTrack(string name)
        {
            var tracks = _context.Tracks
                         .Where(a => a.Musician.Contains(name))
                         .ToList();

            if (tracks.Any())
            {
                return View("Discography", tracks);
            }
            else
            {
                ViewBag.Message = "Исполнитель не найден.";
                return View();
            }
        }
    }
}
