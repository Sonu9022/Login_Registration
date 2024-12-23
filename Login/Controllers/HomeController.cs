using Login.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Login.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly sonuContext users;

        public HomeController(ILogger<HomeController> logger, sonuContext users)
        {
            _logger = logger;
            this.users = users;
        }

        public IActionResult Index()
        {
            var userdata = users.Users.ToList();
            return View(userdata);
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User usr)
        {
            if (ModelState.IsValid)
            {
                var usrdata = users.Users.Add(usr);
                users.SaveChanges();
                TempData["Register"] = "User Registered Successfully";
                return RedirectToAction("Login", "Home");
            }

            return View(usr);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var usrdata = users.Users.Find(id);
            return View(usrdata);
        }
        [HttpPost]
        public IActionResult Edit(int? id, User usr)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                users.Users.Update(usr);
                users.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(usr);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User usr)
        {
            var udata = users.Users.Where(x => x.Email == usr.Email && x.Password == usr.Password).FirstOrDefault();
            if (udata != null)
            {
                HttpContext.Session.SetString("mySession", udata.Name);
                TempData["user"] = HttpContext.Session.GetString("mySession");
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMsg = "Failed to Login";
            }
            return View();
        }


        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("mySession") != null)
            {
                HttpContext.Session.Remove("mySession");
            }
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
