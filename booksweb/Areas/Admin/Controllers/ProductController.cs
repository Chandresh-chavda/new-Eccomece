using books.DataAccess;
using Books.DataAccess.Repository;
using Books.DataAccess.Repository.IRepository;
using Books.Model;
using Books.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Collections.Generic;
using System.Net;

namespace booksweb.Areas.Admin.Controllers;
[Area("Admin")]

public class ProductController : Controller
{
    private readonly IUnitOfWork _unitofwork;
    private readonly IWebHostEnvironment _hostEnvironment;
	public ProductController(IUnitOfWork unitofwork, IWebHostEnvironment hostEnvironment)
	{
        _unitofwork = unitofwork;
        _hostEnvironment = hostEnvironment;

    }
    public IActionResult Index()
    {
       
        return View();
    }
    
    //GET
    public IActionResult Upset(int? id)
    {
        ProductVM productVM = new()
        {
            Product = new(),
            CategoryList = _unitofwork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
            CoverTypeList = _unitofwork.CoverType.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
        };

        
        if (id == null || id == 0)
        {
            //Create
            //ViewBag.CategoryList= CategoryList;
            //ViewBag.CoverTypeList = CoverTypeList;
            //ViewData["CoverTypeList"] = CoverTypeList;

            return View(productVM);
        }
        else
        {
            //Update
            productVM.Product = _unitofwork.Product.GetFirstOrDefault(u => u.Id == id);
            return View(productVM);
        }
        
       
    }
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upset(ProductVM obj, IFormFile? file)
    {
       
        if (ModelState.IsValid)
        {
			string wwwRootPath = _hostEnvironment.WebRootPath;
			if (file != null)
			{
				
				string fileName = Guid.NewGuid().ToString();
				var uploads = Path.Combine(wwwRootPath, @"images\product");
				var extension = Path.GetExtension(file.FileName);

                if (obj.Product.ImageUrl != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
				{
					file.CopyTo(fileStreams);
				}
				obj.Product.ImageUrl = @"\images\product\" + fileName + extension ;

			}
			
			if (obj.Product.Id == 0)
			{
				_unitofwork.Product.Add(obj.Product);
				_unitofwork.Save();
				TempData["success"] = "Product created Successfully";
			}
			else
			{
				_unitofwork.Product.Update(obj.Product);
				_unitofwork.Save();
				TempData["success"] = "Product Updated Successfully";
			}
			
            return RedirectToAction("Index");
        }
        return View(obj);
    }

   
    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        var productList = _unitofwork.Product.GetAll(includeProperties:"Category,CoverType");
        return Json(new { data = productList });
    }

	//POST
	[HttpDelete]
	
	public IActionResult Delete(int? id)
	{
		var obj = _unitofwork.Product.GetFirstOrDefault(u => u.Id == id);
		{
			if (obj == null)
			{
                return Json(new { success = false, Message = "Error While deleting" });
			}
		}
		var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
		if (System.IO.File.Exists(oldImagePath))
		{
			System.IO.File.Delete(oldImagePath);
		}

		_unitofwork.Product.Remove(obj);
		_unitofwork.Save();
		return Json(new { success = true, Message = "Product Deleted" });
		


	}
	#endregion
}

