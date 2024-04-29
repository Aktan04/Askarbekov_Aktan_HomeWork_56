using AuthProduct.Models;
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
    
    public IActionResult Index()
    {
        List<Order> orders = _db.Orders.Include(o => o.Phone).ToList();
        return View(orders);
    }
    
    public IActionResult Create(int phoneId)
    {
        Phone phone = _db.Phones.FirstOrDefault(p => p.Id == phoneId);
        return View(new Order{Phone = phone});
    }
    
    [HttpPost]
    public IActionResult Create(Order order)
    {
        if (ModelState.IsValid)
        {
            _db.Orders.Add(order);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(order);
    }
}