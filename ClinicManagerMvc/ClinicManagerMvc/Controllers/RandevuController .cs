using ClinicManagerMvc.Data;
using ClinicManagerMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ClinicManagerMvc.Controllers
{
    [Authorize] // giriş yapmamış kullanıcı hiçbir action'a erişemez
    public class RandevuController : Controller
    {
        private readonly MvcDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public RandevuController(MvcDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var randevular = await _context.Randevular.Include(r => r.Poliklinik).ToListAsync();
            return View(randevular);
        }

        [HttpGet]
        public async Task<IActionResult> HastaListesi()
        {
            var client = _httpClientFactory.CreateClient("ClinicApi");
            var response = await client.GetAsync("Hasta/GetHastalar");
            var json = await response.Content.ReadAsStringAsync();
            return Content(json, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> DoktorListesi()
        {
            var client = _httpClientFactory.CreateClient("ClinicApi");
            var response = await client.GetAsync("Doktor/GetDoktorlar");
            var json = await response.Content.ReadAsStringAsync();
            return Content(json, "application/json");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Poliklinikler = await _context.Poliklinikler.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Randevu randevu)
        {
            _context.Randevular.Add(randevu);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }
        [HttpPost]
       

        [Authorize(Roles = "Admin")] // sadece Admin silebilir
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu == null) return NotFound();

            _context.Randevular.Remove(randevu);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

