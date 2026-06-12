using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalKibica.Data;
using PortalKibica.Models;

namespace PortalKibica.Controllers.Admin
{
    [Authorize]
    [Area("Admin")]
    [Route("Admin/News")]
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public NewsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // Lista newsów
        public async Task<IActionResult> Index()
        {
            var news = await _context.News
                .OrderByDescending(n => n.PublishDate)
                .ToListAsync();
            return View(news);
        }

        // Formularz dodawania
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // Zapisanie nowego newsa
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(News news, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("BŁĄD: " + error.ErrorMessage);
                }
                return View(news);
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images", "news");
                Directory.CreateDirectory(uploadsFolder);
                string uniqueFileName = Guid.NewGuid() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await imageFile.CopyToAsync(fileStream);
                news.ImagePath = "/images/news/" + uniqueFileName;
            }

            news.PublishDate = DateTime.Now;
            _context.Add(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Formularz edycji
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null) return NotFound();
            return View(news);
        }

        // Zapisanie edycji
        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, News news, IFormFile? imageFile)
        {
            if (id != news.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images", "news");
                    Directory.CreateDirectory(uploadsFolder);
                    string uniqueFileName = Guid.NewGuid() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    await imageFile.CopyToAsync(fileStream);
                    news.ImagePath = "/images/news/" + uniqueFileName;
                }

                _context.Update(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // Potwierdzenie usunięcia
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null) return NotFound();
            return View(news);
        }

        // Usunięcie
        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}