using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropEase.Context;
using PropEase.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PropEase.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IConfiguration _configuration;
        // GET: AuthController

        public AuthController(AppDBContext context, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _context= context;
            _configuration= configuration;
        }
        public ActionResult Login()
        {
            return View();
        }
        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
        //{
        //    var hashedPassword=PasswordHasher.HashPassword(loginModel.Password);
        //    var user = await _context.UsersMsg.FirstOrDefaultAsync(u => u.Username == loginModel.Username && u.PasswordHash == hashedPassword);
        //    if(user==null)
        //    {
        //        ViewBag.ErrorMessage = "Invalid username or password";
        //        return View();
        //    }
        //    var token = JwtTokenGenerator.GenerateToken(
        //        user.Username,
        //        _configuration["Jwt:Key"],
        //         _configuration["Jwt:Issuer"],
        //          _configuration["Jwt:Audience"]
        //        );
        //    Response.Cookies.Append("jwtToken", token, new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = DateTime.UtcNow.AddHours(1)
        //    });
        //    return Ok(new { token });
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
        {
            var hashedPassword = PasswordHasher.HashPassword(loginModel.Password);
            var user = await _context.UsersMsg.FirstOrDefaultAsync(u => u.Username == loginModel.Username && u.PasswordHash == hashedPassword);
            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid username or password";
                return View();
            }

            var userRoles = await _context.UserRoles.Where(ur => ur.UserId == user.id)
                .Select(ur => ur.Role.RoleName).ToListAsync();

            //Create claims

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim("UserId",user.id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            foreach(var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = JwtTokenGenerator.GenerateToken(
                claims,
                _configuration["Jwt:Key"],
                 _configuration["Jwt:Issuer"],
                  _configuration["Jwt:Audience"]
                );
            Response.Cookies.Append("jwtToken", token, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddHours(1)
            });
            return Ok(new { token });
        }
        // GET: AuthController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AuthController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuthController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuthController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
