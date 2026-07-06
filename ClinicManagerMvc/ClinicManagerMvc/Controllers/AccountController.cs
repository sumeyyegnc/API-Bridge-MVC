using ClinicManagerMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagerMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login() => View();

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string kullaniciAdi, string sifre)
        {
            var user = await _userManager.FindByNameAsync(kullaniciAdi);
            if (user == null)
            {
                ViewBag.Hata = "Kullanıcı bulunamadı";
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, sifre, false, false);
            if (!result.Succeeded)
            {
                ViewBag.Hata = "Şifre hatalı";
                return View();
            }

            return RedirectToAction("Index", "Randevu");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register() => View();

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(string kullaniciAdi, string adSoyad, string sifre, string rol)
        {
            var user = new User { UserName = kullaniciAdi, NormalizedUserName = adSoyad };
            var result = await _userManager.CreateAsync(user, sifre);

            if (!result.Succeeded)
            {
                ViewBag.Hata = string.Join(", ", result.Errors.Select(e => e.Description));
                return View();
            }

            if (!await _roleManager.RoleExistsAsync(rol))
                await _roleManager.CreateAsync(new IdentityRole(rol));

            await _userManager.AddToRoleAsync(user, rol);

            return RedirectToAction("Login");
        }

        [Authorize] // çıkış yapmak için zaten giriş yapmış olmak gerekir
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied() => View();
    }
}