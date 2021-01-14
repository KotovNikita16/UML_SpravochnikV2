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
        public IActionResult Index(string SortType = "No", string Code = "No", string DiagramOrElement = "No")
        {
            IEnumerable<UMLElement> els = _context.UMLElements;
            switch (SortType)
            {
                case "Az":
                    els = from e in els
                          orderby e.Title
                          select e;
                    break;
                case "Za":
                    els = els.OrderByDescending(e => e.Title);
                    break;
                default:
                    break;
            }
            switch (Code)
            {
                case "Code":
                    els = from e in els
                          where e.Code != null
                          select e;
                    break;
                case "Without":
                    els = from e in els
                          where e.Code == null
                          select e;
                    break;
                default:
                    break;
            }
            switch (DiagramOrElement)
            {
                case "Diagram":
                    els = from e in els
                          where e.DiagramOrElement == "Diagram"
                          select e;
                    break;
                case "Element":
                    els = from e in els
                          where e.DiagramOrElement == "Element"
                          select e;
                    break;
                default:
                    break;
            }
            ViewData["SortType"] = SortType;
            ViewData["Code"] = Code;
            ViewData["DiagramOrElement"] = DiagramOrElement;
            return View(els);
        }
    }
}
