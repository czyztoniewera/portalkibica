using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalKibica.Data;
using PortalKibica.Models;

namespace PortalKibica.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lista newsów (publiczna)
        public async Task<IActionResult> Index(string? search)
        {
            var newsQuery = _context.News
                .OrderByDescending(n => n.PublishDate)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                newsQuery = newsQuery.Where(n =>
                    n.Title.Contains(search) || n.Content.Contains(search));
            }

            ViewData["CurrentSearch"] = search;

            var news = await newsQuery.ToListAsync();
            return View(news);
        }

        // Szczegóły jednego newsa
        public async Task<IActionResult> Details(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news == null) return NotFound();
            return View(news);
        }
    }
}