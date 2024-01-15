using Business.DAL;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Globalization;

namespace Business.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        AppDbContext _db;

        public SettingController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Setting> list = _db.Settings.ToList();

            return View(list);
        }

        [HttpPost]
        public IActionResult Update(int id)
        {
            if (!_db.Settings.Any(p => p.Id == id))
            {
                return RedirectToAction("Index");
            }
            Setting setting = _db.Settings.Where(p => p.Id == id).FirstOrDefault();
            return View(setting);
        }
        [HttpPost]
        public IActionResult Update(Setting newSetting)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Setting oldSetting = _db.Settings.Find(newSetting.Key);
            if (oldSetting == null)
            {
                return View();
            }
            oldSetting.Value = char.ToUpper(newSetting.Value[0]) + newSetting.Value.Substring(1);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
     
    }
}
