using AuthProduct.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthProduct.Controllers;

public class OrderController : Controller
{
    private MobileContext _db;
    
    public OrderController(MobileContext db)
    {
        _db = db;
    }
    [Authorize(Roles = "user, admin")]
    public IActionResult Index()
    {
        List<Order> orders = _db.Orders.Include(o => o.Phone).Include(u => u.User).ToList();
        if (User.Identity.IsAuthenticated)
        {
            if (User.IsInRole("user"))
            {
                string userName = User.Identity.Name;
                var user = _db.Users.FirstOrDefault(u => u.UserName == userName);
                orders = _db.Orders.Include(o => o.Phone).Where(o => o.Name == user.UserName).ToList();
            }
        }
        return View(orders);
    }
    [Authorize(Roles = "user")]
    public IActionResult Create(int phoneId)
    {
        Phone phone = _db.Phones.FirstOrDefault(p => p.Id == phoneId);
        User user = _db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
        return View(new Order{Phone = phone, User = user});
    }
    
    [HttpPost]
    public IActionResult Create(Order order)
    {
        string userName = User.Identity.Name;
        var user = _db.Users.FirstOrDefault(u => u.UserName == userName);
        if (user != null)
        {
            order.Name = user.UserName;
            order.UserId = user.Id;
        }

        order.DateOfCreating = DateTime.UtcNow;
        if (ModelState.IsValid)
        {
            _db.Orders.Add(order);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(order);
    }
}