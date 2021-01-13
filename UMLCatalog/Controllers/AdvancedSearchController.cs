using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UMLCatalog.Models;
using UMLCatalog.Contexts;

namespace UMLCatalog.Controllers
{
    public class AdvancedSearchController : Controller
    {
        UMLCatalogContext _context;
        public AdvancedSearchController(UMLCatalogContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchString)
        {
            return View(_context.UMLElements);
        }
    }
}
