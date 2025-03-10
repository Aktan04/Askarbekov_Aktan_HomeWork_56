using AuthProduct.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthProduct.Controllers;

public class PhoneController : Controller
{
    private MobileContext _db;

    public PhoneController(MobileContext db)
    {
        _db = db;
    }
    
    public IActionResult Index()
    {
        List<Phone> phones = _db.Phones.Include(b => b.Brand).ToList();
        return View(phones);
    }
    [Authorize(Roles = "admin")]
    public IActionResult Create()
    {
        ViewBag.Brands = _db.Brands.ToList();
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Phone phone)
    {
        if (_db.Phones.Any(c => c.Name == phone.Name))
        {
            ModelState.AddModelError("Name", "Телефон с таким названием уже существует!");
            return View(phone);
        }
        if (ModelState.IsValid)
        {
            _db.Phones.Add(phone);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    
        ViewBag.Brands = _db.Brands.ToList();
        return View(phone);
    }
    [Authorize(Roles = "admin")]
    public IActionResult Edit(int? id)
    {
        
        if (id != null)
        {
            Phone phone = _db.Phones.FirstOrDefault(p => p.Id == id);
            if (phone != null)
            {
                ViewBag.Brands = _db.Brands.ToList();
                return View(phone);
            }
        }
        return NotFound();
    }
    
    [HttpPost]
    public IActionResult Edit(Phone phone, int id)
    {
        if (_db.Phones.Any(c => c.Name == phone.Name && c.Id != id))
        {
            ModelState.AddModelError("Name", "Телефон с таким названием уже существует!");
            return View(phone);
        }
        ViewBag.Brands = _db.Brands.ToList();
        phone.DateOfEditing = DateTime.UtcNow;
        if (ModelState.IsValid)
        {
            _db.Phones.Update(phone);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(phone);
    }
    [Authorize(Roles = "admin")]
    public IActionResult Delete(int? id)
    {
        if (id != null)
        {
            Phone phone = _db.Phones.FirstOrDefault(p => p.Id == id);
            if (phone != null)
            {
                return View(phone);
            }
        }
        return NotFound();
    }
    
    [HttpPost]
    public IActionResult ConfirmDelete(int? id)
    {
        if (id != null)
        {
            Phone phone = _db.Phones.FirstOrDefault(p => p.Id == id);
            if (phone != null)
            {
                _db.Phones.Remove(phone);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        return NotFound();
    }
    [Authorize(Roles = "admin, user")]
    public IActionResult Details(int id)
    {
        var product = _db.Phones.Include(r => r.Reviews).Include(b=>b.Brand).FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        
        return View(product);
    }
}