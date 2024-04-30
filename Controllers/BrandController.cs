using AuthProduct.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthProduct.Controllers;

public class BrandController : Controller
{
    private readonly MobileContext _db;

    public BrandController(MobileContext db)
    {
        _db = db;
    }

    [Authorize(Roles = "admin, user")]
    public IActionResult Index()
    {
        var brands = _db.Brands.ToList();
        return View(brands);
    }
    [Authorize(Roles = "admin")]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Brand brand)
    {
        if (_db.Brands.Any(c => c.Name == brand.Name))
        {
            ModelState.AddModelError("Name", "Бренд с таким названием уже существует!");
            return View(brand);
        }
        brand.FoundationDate = brand.FoundationDate.ToUniversalTime();
        if (ModelState.IsValid)
        {
            _db.Brands.Add(brand);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(brand);
    }
    [Authorize(Roles = "admin")]
    public IActionResult Edit(int id)
    {
        var brand = _db.Brands.Find(id);
        if (brand == null)
        {
            return NotFound();
        }
        return View(brand);
    }

    [HttpPost]
    public IActionResult Edit(Brand brand, int id)
    {
        if (_db.Brands.Any(c => c.Name == brand.Name && c.Id != id))
        {
            ModelState.AddModelError("Name", "Категория с таким названием уже существует!");
            return View(brand);
        }
        brand.FoundationDate = brand.FoundationDate.ToUniversalTime();
        if (ModelState.IsValid)
        {
            _db.Update(brand);
            _db.SaveChanges();
            return RedirectToAction("Index", "Brand");
        }
        return View(brand);
    }
    [Authorize(Roles = "admin")]
    public IActionResult Delete(int id)
    {
        var brand = _db.Brands.FirstOrDefault(b => b.Id == id);
        if (brand == null)
        {
            return NotFound();
        }
        _db.Brands.Remove(brand);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
}