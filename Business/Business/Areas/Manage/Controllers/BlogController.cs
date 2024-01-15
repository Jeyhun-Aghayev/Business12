using AutoMapper;
using Business.Areas.Manage.ViewModels.Blog;
using Business.DAL;
using Business.Helpers;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Business.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _web;

        public BlogController(AppDbContext db, IMapper mapper, IWebHostEnvironment web)
        {
            _db = db;
            _mapper = mapper;
            _web = web;
        }

        public async Task<IActionResult> Index()
        {
            List<GetBlogVm> VmList = _mapper.Map<List<GetBlogVm>>(await _db.Blogs.Where(b => b.IsDeleted == false).ToListAsync());
            return View(VmList);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Blog blog = await _db.Blogs.Where(b => b.IsDeleted == false && b.Id == id).FirstOrDefaultAsync();
            if (blog is null) return BadRequest();
            UpdateBlogVm vm = new UpdateBlogVm()
            {
                Id = id,
                Title = blog.Title,
                Description = blog.Description,
                imageUrl = blog.ImgUrl
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateBlogVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            if(vm.Image is not null) if (!CheckImage(vm.Image)) return View(vm);
            var blog = await _db.Blogs.Where(b => b.IsDeleted == false && b.Id == vm.Id).FirstOrDefaultAsync();
            blog.Id = vm.Id;
            blog.Title = vm.Title;
            blog.Description = vm.Description;
            blog.UpdatedAt = DateTime.Now;
            if (vm.Image is not null) blog.ImgUrl = vm.Image.UploadForUpdate(_web.WebRootPath, @"\Upload\Image\");
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (!CheckId(id)) return BadRequest();
            var blog = await _db.Blogs.Where(b => b.Id == id && !b.IsDeleted).FirstOrDefaultAsync();
            if (blog == null) return BadRequest();
            blog.IsDeleted = true;
            await _db.SaveChangesAsync();   
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogVm vm)
        {
            if(!ModelState.IsValid) return View(vm);
            if(!CheckImage(vm.Image)) return View(vm);
            Blog blog = new Blog()
            {
                Title= vm.Title,
                Description = vm.Description,
                ImgUrl = vm.Image.Upload(_web.WebRootPath,@"\Upload\Image\"),
                CreatedAt = DateTime.Now,
            };
            await _db.Blogs.AddAsync(blog);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public bool CheckImage(IFormFile file)
        {
            if (!file.ContentType.Contains("image"))
            {
                ModelState.AddModelError("MainlImages", "Only images can be uploaded");
                return false;
            }
            if (file.Length > 2097152)
            {
                ModelState.AddModelError("MainImages", "Maximum 2 MB images can be uploaded");
                return false;
            }

            return true;
        }
        public bool CheckId(int id)
        {
            if(id<=0) return false;
            return true;
        }
    }
}
