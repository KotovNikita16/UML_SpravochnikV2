using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UMLCatalog.Models;
using UMLCatalog.Contexts;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace UMLCatalog.Controllers
{
    public class UMLElementController : Controller
    {
        UMLCatalogContext db;
        public UMLElementController(UMLCatalogContext context)
        {
            db = context;
        }
        public IActionResult Index(int id)
        {
            return View(db.UMLElements.Find(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UMLElement el)
        {
            if (ModelState.IsValid)
            {

                using (var binaryReader = new BinaryReader(el.File.OpenReadStream()))
                {
                    el.Picture = binaryReader.ReadBytes((int)el.File.Length);
                    el.ImgType = el.File.ContentType;
                }
                db.Add(el);
                db.SaveChanges();
                return RedirectToAction("Index", new { el.Id });
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                var el = db.UMLElements.Find(id);
                UMLEdit edit = new UMLEdit { Id = el.Id, Title = el.Title, Description = el.Description, Code = el.Code, Tags = el.Tags };
                return View(edit);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Edit(UMLEdit edit)
        {
            if (ModelState.IsValid)
            {
                var el = db.UMLElements.Find(edit.Id);
                if (edit.File != null)
                {
                    el.File = edit.File;
                    using (var binaryReader = new BinaryReader(el.File.OpenReadStream()))
                    {
                        el.Picture = binaryReader.ReadBytes((int)el.File.Length);
                        el.ImgType = el.File.ContentType;
                    }
                }
                el.Title = edit.Title;
                el.Description = edit.Description;
                el.Code = edit.Code;
                el.Tags = edit.Tags;
                db.UMLElements.Update(el);
                db.SaveChanges();
                ViewBag.SuccessMessage = "Edited";
                return RedirectToAction("Index", new { el.Id });
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id != null)
            {
                UMLElement el = db.UMLElements.Find(id);
                if (el != null)
                    return View(el);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                UMLElement el = db.UMLElements.Find(id);
                db.UMLElements.Remove(el);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return NotFound();
        }
    }
}
