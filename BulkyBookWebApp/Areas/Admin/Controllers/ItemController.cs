﻿using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ItemController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Item> objItemList = _unitOfWork.Item.GetAll().ToList();

            return View(objItemList);
        }

        public IActionResult UpSert(int? id)   //Update + Insrert
        {

            //passing category List to the View using ViewBAG          
            //ViewBag.CategoryList = CategoryList;

            //passing category List to the View using ViewData          
            //  ViewData["CategoryList"] = CategoryList;

            ItemVM itemVM = new ItemVM();

            itemVM.CategoryList = _unitOfWork.Category.
              GetAll().Select(x => new SelectListItem
              {
                  Text = x.Name,
                  Value = x.Id.ToString()
              });
            itemVM.Item = new Item();

            if (id == null || id == 0)
            {
                //create

                return View(itemVM);
            }
            else
            {
                //update

                itemVM.Item = _unitOfWork.Item.Get(x => x.Id == id);
                return View(itemVM);
            }
        }

        [HttpPost]
        public IActionResult UpSert(ItemVM itemVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string itemPath = Path.Combine(wwwRootPath, @"images\items");

                    if (!string.IsNullOrEmpty(itemVM.Item.ImageURL))
                    {
                        //delete the old Image

                        var oldImagePath = Path.Combine(wwwRootPath, itemVM.Item.ImageURL.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }


                    using(var fileStream=new FileStream(Path.Combine(itemPath, fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    itemVM.Item.ImageURL = @"\images\items\" + fileName;
                }

                if (itemVM.Item.Id == 0)
                {
                    //Create New Item
                    _unitOfWork.Item.Add(itemVM.Item);
                }
                else
                {
                    //Update
                    _unitOfWork.Item.Update(itemVM.Item);
                }

                
                _unitOfWork.Save();
                TempData["success"] = "Item created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                itemVM.CategoryList = _unitOfWork.Category.
                  GetAll().Select(x => new SelectListItem
                  {
                      Text = x.Name,
                      Value = x.Id.ToString()
                  });

                return View(itemVM);
            }
        }

        //Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Item? itemFromDB = _unitOfWork.Item.Get(x => x.Id == id);

            if (itemFromDB == null)
            {
                return NotFound();
            };

            return View(itemFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Item? obj = _unitOfWork.Item.Get(x => x.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Item.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Item deleted successfully";
            return RedirectToAction("Index");
        }
    }
}