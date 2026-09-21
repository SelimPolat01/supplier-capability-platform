using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupplierCapabilitiesAndManagementSystem.Entities;
using SupplierCapabilitiesAndManagementSystem.Models.DTO;
using SupplierCapabilitiesAndManagementSystem.Services;

namespace SupplierCapabilitiesAndManagementSystem.Controllers
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

            var passwordSignInResult = await _signInManager.PasswordSignInAsync(user, loginPostDTO.Password, isPersistent: loginPostDTO.RememberMe, lockoutOnFailure: true);

            if (passwordSignInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "The 5-minute lockout period has been triggered due to too many incorrect entries for the parts.");
                return View(loginPostDTO);
            }

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
                "Qualitier" => RedirectToAction("Home", "Qualitier"),
                "Purchaser" => RedirectToAction("Home", "Purchaser"),
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

            return RedirectToAction("Login", "Auth");
        }

        [HttpGet("qualitier-register")]
        public IActionResult QualitierRegister()
        {
            var redirect = RedirectBasedOnRole();
            if (redirect != null) return redirect;

            return View();
        }

        [HttpPost("qualitier-register")]
        public async Task<IActionResult> QualitierRegister(QualitierRegisterPostRequestDTO qualitierRegisterPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(qualitierRegisterPostDTO);
            }

            var existingUser = await _userManager.FindByEmailAsync(qualitierRegisterPostDTO.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "This email address is already registered.");
                return View(qualitierRegisterPostDTO);
            }

            AppUser newUser = new()
            {
                Name = qualitierRegisterPostDTO.Name,
                Surname = qualitierRegisterPostDTO.Surname,
                Email = qualitierRegisterPostDTO.Email,
                UserName = qualitierRegisterPostDTO.Email
            };

            var result = await _userManager.CreateAsync(newUser, qualitierRegisterPostDTO.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(qualitierRegisterPostDTO);
            }

            if (!await _roleManager.RoleExistsAsync("Qualitier"))
            {
                AppRole role = new() { Name = "Qualitier" };
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(newUser, "Qualitier");

            return RedirectToAction("Login", "Auth");
        }

        [HttpGet("purchaser-register")]
        public IActionResult PurchaserRegister()
        {
            var redirect = RedirectBasedOnRole();
            if (redirect != null) return redirect;

            return View();
        }

        [HttpPost("purchaser-register")]
        public async Task<IActionResult> PurchaserRegister(PurchaserRegisterPostRequestDTO PpurchaserRegisterPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(PpurchaserRegisterPostDTO);
            }

            var existingUser = await _userManager.FindByEmailAsync(PpurchaserRegisterPostDTO.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "This email address is already registered.");
                return View(PpurchaserRegisterPostDTO);
            }

            AppUser newUser = new()
            {
                Name = PpurchaserRegisterPostDTO.Name,
                Surname = PpurchaserRegisterPostDTO.Surname,
                Email = PpurchaserRegisterPostDTO.Email,
                UserName = PpurchaserRegisterPostDTO.Email
            };

            var result = await _userManager.CreateAsync(newUser, PpurchaserRegisterPostDTO.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(PpurchaserRegisterPostDTO);
            }

            if (!await _roleManager.RoleExistsAsync("Purchaser"))
            {
                AppRole role = new() { Name = "Purchaser" };
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(newUser, "Purchaser");

            return RedirectToAction("Login", "Auth");
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

            return RedirectToAction("Login", "Auth");
        }

        [HttpPost("log-out")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            Response.Cookies.Delete("JwtToken");

            return Ok(new { message = "Successfully logged out." });
        }

        private IActionResult? RedirectBasedOnRole()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin")) return RedirectToAction("Home", "Admin");
                if (User.IsInRole("Qualitier")) return RedirectToAction("Home", "Qualitier");
                if (User.IsInRole("Purchaser")) return RedirectToAction("Home", "Purchaser");
                if (User.IsInRole("Supplier")) return RedirectToAction("Home", "Supplier");
            }
            return null;
        }
    }
}