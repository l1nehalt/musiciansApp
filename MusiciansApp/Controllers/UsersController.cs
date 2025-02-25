using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusiciansApp.Model;
using MusiciansApp.Repository;
using SQLitePCL;

namespace MusiciansApp.Controllers
{
    public class UsersController : Controller
    {

        private readonly MusicDbContext _context;

        public UsersController(MusicDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if (_context.Users.Any(a => a.UserName == user.UserName || a.Email == user.Email))
            {
                ModelState.AddModelError("", " Пользователь с таким именем или email уже существует.");
                return View(user);
            }

            _context.Users.Add(user);
            _context.SaveChanges();
            _context.Database.ExecuteSqlRaw("UPDATE sqlite_sequence SET seq = 1 WHERE name = 'users'");


            TempData["SuccessMessageRegister"] = "Регистрация прошла успешно!";

            return RedirectToAction("Login", "Users");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string name, string password)
        {
            var user = _context.Users.FirstOrDefault(a => a.UserName == name && a.Password == password);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Неверное имя пользователя или пароль.";
                return View();
            }

            TempData["UserName"] = user.UserName;
            TempData["SuccessMessageLogin"] = "Вы успешно вошли в систему!";

            return RedirectToAction("SongDetails", "Songs");
        }
    }
}
