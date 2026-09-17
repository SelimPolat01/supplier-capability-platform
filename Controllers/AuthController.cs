using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TedarikciKabiliyetYonetimSistemi.Entities;
using TedarikciKabiliyetYonetimSistemi.Models.DTO;
using TedarikciKabiliyetYonetimSistemi.Services;

namespace TedarikciKabiliyetYonetimSistemi.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpGet("login")]
        [HttpGet("/")]
        public IActionResult Login()
        {
            var redirect = RedirectBasedOnRole();
            if (redirect != null) return redirect;

            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginPostRequestDTO loginPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(loginPostDTO);
            }

            var user = await _userManager.FindByEmailAsync(loginPostDTO.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email", "Invalid email.");
                return View(loginPostDTO);
            }

            var passwordSignInResult = await _signInManager.PasswordSignInAsync(user, loginPostDTO.Password, isPersistent: loginPostDTO.RememberMe, lockoutOnFailure: false);

            if (!passwordSignInResult.Succeeded)
            {
                ModelState.AddModelError("Password", "Invalid password.");
                return View(loginPostDTO);
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            string role = userRoles.FirstOrDefault() ?? "Supplier";

            string userId = Convert.ToString(user.Id);
            string email = user.Email ?? string.Empty;

            string token = _tokenService.GenerateToken(userId, email, role, loginPostDTO.RememberMe);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = loginPostDTO.RememberMe ? DateTime.UtcNow.AddDays(14) : DateTime.UtcNow.AddMinutes(60)
            };

            Response.Cookies.Append("JwtToken", token, cookieOptions);

            return role switch
            {
                "Admin" => RedirectToAction("Home", "Admin"),
                "Quality" => RedirectToAction("Home", "Quality"),
                "Purchasing" => RedirectToAction("Home", "Purchasing"),
                "Supplier" => RedirectToAction("Home", "Supplier"),
                _ => RedirectToAction("Home", "Admin")
            };
        }

        [HttpGet("admin-register")]
        public IActionResult AdminRegister()
        {
            var redirect = RedirectBasedOnRole();
            if (redirect != null) return redirect;

            return View();
        }

        [HttpPost("admin-register")]
        public async Task<IActionResult> AdminRegister(AdminRegisterPostRequestDTO adminRegisterPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(adminRegisterPostDTO);
            }

            var existingUser = await _userManager.FindByEmailAsync(adminRegisterPostDTO.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "This email address is already registered.");
                return View(adminRegisterPostDTO);
            }

            AppUser newUser = new()
            {
                Name = adminRegisterPostDTO.Name,
                Surname = adminRegisterPostDTO.Surname,
                Email = adminRegisterPostDTO.Email,
                UserName = adminRegisterPostDTO.Email
            };

            var result = await _userManager.CreateAsync(newUser, adminRegisterPostDTO.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(adminRegisterPostDTO);
            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                AppRole role = new() { Name = "Admin" };
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(newUser, "Admin");

            return RedirectToAction("Login");
        }

        [HttpGet("quality-register")]
        public IActionResult QualityRegister()
        {
            var redirect = RedirectBasedOnRole();
            if (redirect != null) return redirect;

            return View();
        }

        [HttpPost("quality-register")]
        public async Task<IActionResult> QualityRegister(QualityRegisterPostRequestDTO qualityRegisterPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(qualityRegisterPostDTO);
            }

            var existingUser = await _userManager.FindByEmailAsync(qualityRegisterPostDTO.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "This email address is already registered.");
                return View(qualityRegisterPostDTO);
            }

            AppUser newUser = new()
            {
                Name = qualityRegisterPostDTO.Name,
                Surname = qualityRegisterPostDTO.Surname,
                Email = qualityRegisterPostDTO.Email,
                UserName = qualityRegisterPostDTO.Email
            };

            var result = await _userManager.CreateAsync(newUser, qualityRegisterPostDTO.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(qualityRegisterPostDTO);
            }

            if (!await _roleManager.RoleExistsAsync("Quality"))
            {
                AppRole role = new() { Name = "Quality" };
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(newUser, "Quality");

            return RedirectToAction("Login");
        }

        [HttpGet("purchasing-register")]
        public IActionResult PurchasingRegister()
        {
            var redirect = RedirectBasedOnRole();
            if (redirect != null) return redirect;

            return View();
        }

        [HttpPost("purchasing-register")]
        public async Task<IActionResult> PurchasingRegister(PurchasingRegisterPostRequestDTO purchasingRegisterPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(purchasingRegisterPostDTO);
            }

            var existingUser = await _userManager.FindByEmailAsync(purchasingRegisterPostDTO.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "This email address is already registered.");
                return View(purchasingRegisterPostDTO);
            }

            AppUser newUser = new()
            {
                Name = purchasingRegisterPostDTO.Name,
                Surname = purchasingRegisterPostDTO.Surname,
                Email = purchasingRegisterPostDTO.Email,
                UserName = purchasingRegisterPostDTO.Email
            };

            var result = await _userManager.CreateAsync(newUser, purchasingRegisterPostDTO.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(purchasingRegisterPostDTO);
            }

            if (!await _roleManager.RoleExistsAsync("Purchasing"))
            {
                AppRole role = new() { Name = "Purchasing" };
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(newUser, "Purchasing");

            return RedirectToAction("Login");
        }

        [HttpGet("supplier-register")]
        public IActionResult SupplierRegister()
        {
            var redirect = RedirectBasedOnRole();
            if (redirect != null) return redirect;

            return View();
        }

        [HttpPost("supplier-register")]
        public async Task<IActionResult> SupplierRegister(SupplierRegisterPostRequestDTO supplierRegisterPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(supplierRegisterPostDTO);
            }

            var existingUser = await _userManager.FindByEmailAsync(supplierRegisterPostDTO.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "This email address is already registered.");
                return View(supplierRegisterPostDTO);
            }

            AppUser newUser = new()
            {
                Name = supplierRegisterPostDTO.Name,
                Surname = supplierRegisterPostDTO.Surname,
                CompanyName = supplierRegisterPostDTO.CompanyName,
                Email = supplierRegisterPostDTO.Email,
                UserName = supplierRegisterPostDTO.Email
            };

            var result = await _userManager.CreateAsync(newUser, supplierRegisterPostDTO.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(supplierRegisterPostDTO);
            }

            if (!await _roleManager.RoleExistsAsync("Supplier"))
            {
                AppRole role = new() { Name = "Supplier" };
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(newUser, "Supplier");

            return RedirectToAction("Login");
        }

        [HttpPost("log-out")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return Ok(new { message = "Successfully logged out." });
        }

        private IActionResult? RedirectBasedOnRole()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin")) return RedirectToAction("Home", "Admin");
                if (User.IsInRole("Quality")) return RedirectToAction("Home", "Quality");
                if (User.IsInRole("Purchasing")) return RedirectToAction("Home", "Purchasing");
                if (User.IsInRole("Supplier")) return RedirectToAction("Home", "Supplier");
            }
            return null;
        }
    }
}