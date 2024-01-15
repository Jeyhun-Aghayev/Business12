using Business.DAL;
using Business.Models;
using Business.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Business.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext logger)
        {
            _db = logger;
        }

        public IActionResult Index()
        {
            HomeVm vm = new HomeVm()
            {
                Blogs = _db.Blogs.Where(b=>b.IsDeleted == false).ToList(),
            };
            var logoSetting = _db.Settings.FirstOrDefault(s => s.Key == "Logo");

            ViewData["LogoUrl"] = logoSetting?.Value;
            return View(vm);
        }

    }
}