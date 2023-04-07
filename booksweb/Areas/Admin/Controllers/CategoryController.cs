using books.DataAccess;
using Books.DataAccess.Repository;
using Books.DataAccess.Repository.IRepository;
using Books.Model;
using Microsoft.AspNetCore.Mvc;

namespace booksweb.Areas.Admin.Controllers;
[Area("Admin")]

public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitofwork;

    public CategoryController(IUnitOfWork unitofwork)
    {
        _unitofwork = unitofwork;

    }
    public IActionResult Index()
    {
        IEnumerable<Category> objCategoryList = _unitofwork.Category.GetAll();
        return View(objCategoryList);
    }
    //GET
    public IActionResult Create()
    {

        return View();
    }
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "the display can not exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _unitofwork.Category.Add(obj);
            _unitofwork.Save();
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    //GET
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        //var CategoryfromDb = _db.Categories.Find(id);
        var CategoryFromDbFirst = _unitofwork.Category.GetFirstOrDefault(u => u.Id == id);
        //var CategoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == "Id");

        if (CategoryFromDbFirst == null)
        {
            return NotFound();
        }
        return View(CategoryFromDbFirst);
    }
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "the display can not exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _unitofwork.Category.Update(obj);
            _unitofwork.Save();
            TempData["success"] = "Category Edited Successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    //GET
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        //var CategoryfromDb = _db.Categories.Find(id);
        var CategoryFromDbFirst = _unitofwork.Category.GetFirstOrDefault(u => u.Id == id);
        //var CategoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

        if (CategoryFromDbFirst == null)
        {
            return NotFound();
        }
        return View(CategoryFromDbFirst);
    }
    //POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? Id)
    {
        var obj = _unitofwork.Category.GetFirstOrDefault(u => u.Id == Id);
        {
            if (obj == null)
            {
                return NotFound();
            }
        }

        _unitofwork.Category.Remove(obj);
        _unitofwork.Save();
        TempData["success"] = "Category Deleted Successfully";
        return RedirectToAction("Index");


    }
}

