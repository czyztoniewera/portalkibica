using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalKibica.Data;
using PortalKibica.Models;

namespace PortalKibica.Controllers.Admin
{
    [Authorize]
    [Area("Admin")]
    [Route("Admin/Players")]
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PlayersController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var players = await _context.Players
                .OrderBy(p => p.Number)
                .ToListAsync();
            return View(players);
        }

        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Player player, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images", "players");
                    Directory.CreateDirectory(uploadsFolder);
                    string uniqueFileName = Guid.NewGuid() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    await imageFile.CopyToAsync(fileStream);
                    player.ImagePath = "/images/players/" + uniqueFileName;
                }

                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null) return NotFound();
            return View(player);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Player player, IFormFile? imageFile)
        {
            if (id != player.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images", "players");
                    Directory.CreateDirectory(uploadsFolder);
                    string uniqueFileName = Guid.NewGuid() + "_" + imageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    await imageFile.CopyToAsync(fileStream);
                    player.ImagePath = "/images/players/" + uniqueFileName;
                }

                _context.Update(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null) return NotFound();
            return View(player);
        }

        [HttpPost]
        [Route("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}