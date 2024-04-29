using AuthProduct.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthProduct.Controllers;

public class ValidationController : Controller
{
    private readonly MobileContext _context;

    public ValidationController(MobileContext context)
    {
        _context = context;
    }
    
    [AcceptVerbs("GET", "POST")]
    public bool CheckDate(DateTime foundationDate)
    {
        if (foundationDate > DateTime.Now || foundationDate < DateTime.Now.AddYears(-100))
        {
            return false;
        }

        return true;
    }
}