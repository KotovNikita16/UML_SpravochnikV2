using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UMLCatalog.Contexts;
using UMLCatalog.Models;

namespace UMLCatalog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UMLCatalogContext _context;
        public HomeController(ILogger<HomeController> logger, UMLCatalogContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string searchString)
        {
            IEnumerable<UMLElement> Els = from e in _context.UMLElements
                                         orderby e.Title
                                         select e;
            if (!string.IsNullOrEmpty(searchString))
            {
                Els = from e in Els
                      where e.Title.ToUpper().Contains(searchString.ToUpper())
                      select e;
                ViewData["SearchString"] = searchString;
            }
            return View(Els);
        }

        public IActionResult DescriptionSearch(string searchStringDesc)
        {
            IEnumerable<UMLElement> Els = from e in _context.UMLElements
                                          orderby e.Title
                                          select e;
            if (!string.IsNullOrEmpty(searchStringDesc))
            {
                Els = from e in Els
                      where NulTags(e, searchStringDesc) ||
                      e.Description.ToUpper().Contains(searchStringDesc.ToUpper())
                      select e;
                ViewData["SearchStringDesc"] = searchStringDesc;
            }
            return View(Els);
        }

        private bool NulTags(UMLElement el, string searchStringDesc)
        {
            if (el.Tags == null) return false;
            if (el.Tags.ToUpper().Contains(searchStringDesc.ToUpper())) return true;
            return false;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
