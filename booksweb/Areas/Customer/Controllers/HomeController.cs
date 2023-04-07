using books.DataAccess;
using Books.DataAccess.Repository.IRepository;
using Books.Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace booksweb.Areas.Customer.Controllers;
[Area("Customer")]

public class HomeController : Controller
{
	private readonly IUnitOfWork _unitofwork;
	private readonly IWebHostEnvironment _hostEnvironment;
	public HomeController(IUnitOfWork unitofwork, IWebHostEnvironment hostEnvironment)
	{
		_unitofwork = unitofwork;
		_hostEnvironment = hostEnvironment;

	}

	public IActionResult Index()
    {
        IEnumerable<Product> productList = _unitofwork.Product.GetAll(includeProperties: "Category,CoverType");
        return View(productList);
    }
	public IActionResult Details(int id)

	{
		ShoppingCart Cartobj = new()
		{
			Count=1,
			Product = _unitofwork.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,CoverType")
		};
	
		return View(Cartobj);
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