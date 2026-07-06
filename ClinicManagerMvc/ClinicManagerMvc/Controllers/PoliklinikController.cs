using ClinicManagerMvc.Data;
using ClinicManagerMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ClinicManagerMvc.Controllers
{
    [Authorize]
    public class PoliklinikController : Controller
    {
        private readonly MvcDbContext _context;
        public PoliklinikController(MvcDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var poliklinikler = await _context.Poliklinikler.ToListAsync();
            return View(poliklinikler);
        }

        [HttpGet("PoliklinikTest")]
        public IActionResult Test() => Content("Çalışıyor!");
        [HttpPost]
        public async Task<IActionResult> Create(string ad)
        {
            _context.Poliklinikler.Add(new Poliklinik { Ad = ad });
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var poliklinik = await _context.Poliklinikler.FindAsync(id);
            if (poliklinik == null) return NotFound();

            _context.Poliklinikler.Remove(poliklinik);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}
