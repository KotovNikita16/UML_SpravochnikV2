﻿using System;
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
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
                return View(db.UMLElements.Find(id));
            return NotFound();
        }
        [HttpPost]
        public IActionResult Edit(UMLElement el)
        {
            if (ModelState.IsValid)
            {
                using (var binaryReader = new BinaryReader(el.File.OpenReadStream()))
                {
                    el.Picture = binaryReader.ReadBytes((int)el.File.Length);
                    el.ImgType = el.File.ContentType;
                }
                db.UMLElements.Update(el);
                db.SaveChanges();
                ViewBag.SuccessMessage = "Edited";
                return RedirectToAction("Index", "Home");
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