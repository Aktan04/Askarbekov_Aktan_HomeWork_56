using System.Security.Claims;
using AuthProduct.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthProduct.Controllers;

public class AccountController : Controller
{ 
    private MobileContext _db;

    public AccountController(MobileContext db)
    {
        _db = db;
    }

    [Authorize(Roles = "admin")]
    public IActionResult Index()
    {
        var users = _db.Users.ToList();
        return View(users);
    }
    [HttpGet]
    public IActionResult Login()
    {
       return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        { 
            User? user = await _db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == model.Email || u.UserName == model.Email && u.Password == model.Password);
            if (user != null)
            {
                await Authenticate(user); 
                return RedirectToAction("Index", "Phone");
            }

            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        }

        return View(model);
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email || u.UserName == model.UserName);
            if (user == null)
            {
                Role role = await _db.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                User newUser = new User { Email = model.Email, Password = model.Password,UserName = model.UserName, RoleId = role.Id, Role = role};
                await _db.Users.AddAsync(newUser);
                await _db.SaveChangesAsync();
                if (!User.IsInRole("admin"))
                {
                    await Authenticate(newUser);
                }
                return RedirectToAction("Index", "Phone");
            }
            ModelState.AddModelError("", "Некорректные логин и(или) пароль или пользователь с таким именем или email существует");
        }

        return View(model);
    }
    
    
    public async Task Authenticate(User user)
    { 
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
        };

        ClaimsIdentity id = new ClaimsIdentity(
            claims,
            "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(id),
            new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(10)

            }
        );

    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }
    
    [Authorize(Roles = "admin")]
    public IActionResult Delete(int? id)
    {
        if (id != null)
        {
            User user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                return View(user);
            }
        }
        return NotFound();
    }
    
    [HttpPost]
    public IActionResult ConfirmDelete(int? id)
    {
        if (id != null)
        {
            User user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        return NotFound();
    }
    
    [Authorize(Roles = "admin")]
    public IActionResult Edit(int? id)
    {
        if (id != null)
        {
            User user = _db.Users.FirstOrDefault(u => u.Id == id);
            /*Role role = _db.Roles.FirstOrDefault(r => r.Id == user.RoleId);
            user.Role = role;*/
            if (user != null)
            {
                return View(user);
            }
        }
        return NotFound();
    }
    
    [HttpPost]
    public IActionResult Edit(User user)
    {
        
        if (ModelState.IsValid)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(user);
    }
}