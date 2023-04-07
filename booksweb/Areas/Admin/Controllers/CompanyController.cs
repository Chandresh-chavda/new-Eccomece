using books.DataAccess;
using Books.DataAccess.Repository;
using Books.DataAccess.Repository.IRepository;
using Books.Model;
using Books.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace booksweb.Areas.Admin.Controllers;
[Area("Admin")]

public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitofwork;

    public CompanyController(IUnitOfWork unitofwork)
    {
        _unitofwork = unitofwork;

    }
    public IActionResult Index()
    {
        IEnumerable<CoverType> objCoverTypeList = _unitofwork.CoverType.GetAll();
        return View(objCoverTypeList);
    }
	//GET
	public IActionResult Upset(int? id)
	{
		Company company = new();
		


		if (id == null || id == 0)
		{
			return View(company);
		}
		else
		{
			//Update
			company = _unitofwork.Company.GetFirstOrDefault(u => u.Id == id);
			return View(company);
		}


	}
	//POST
	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Upset(Company obj)
	{

		if (ModelState.IsValid)
		{




			if (obj.Id == 0)
			{
				_unitofwork.Company.Add(obj);
				TempData["success"] = "Company created Successfully";
			}
			else
			{
				_unitofwork.Company.Update(obj);
				TempData["success"] = "Company Updated Successfully";
			}
			_unitofwork.Save();

			return RedirectToAction("Index");
		}
		
		return View(obj);
	}


	#region API CALLS

	[HttpGet]
	public IActionResult GetAll()
	{
		var companyList = _unitofwork.Company.GetAll();
		return Json(new { data = companyList });
	}

	//POST
	[HttpDelete]

	public IActionResult Delete(int? id)
	{
		var obj = _unitofwork.Company.GetFirstOrDefault(u => u.Id == id);
		{
			if (obj == null)
			{
				return Json(new { success = false, Message = "Error While deleting" });
			}
		}
		

		_unitofwork.Company.Remove(obj);
		_unitofwork.Save();
		return Json(new { success = true, Message = "Product Deleted" });



	}
	#endregion
}
