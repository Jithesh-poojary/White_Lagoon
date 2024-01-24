using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.web.Models.ViewModels;

namespace WhiteLagoon.web.Controllers
{
	public class VillaNumberController : Controller
	{
		private readonly ApplicationDbContext _context;

        public VillaNumberController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public IActionResult Index()
		{
			var VillaNumbers=_context.VillaNumbers.ToList();
			return View(VillaNumbers);
		}
        public IActionResult Create()
        {
			VillaNumberVM villaNumberVM = new()
			{
				VillaList = _context.Villas.ToList().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				})
			};
            return View(villaNumberVM);
        }

		[HttpPost]
		public IActionResult Create(VillaNumber obj)
		{
			ModelState.Remove("Villa");
			if (ModelState.IsValid)
			{
				_context.VillaNumbers.Add(obj);
				_context.SaveChanges();
				TempData["success"] = "The Villa Number is Created Successfully..";
				//return RedirectToAction("Index","Villa");     //can also be used controller as a parameter just to identify to which controller it belongs
				return RedirectToAction("Index");
			}
			return View();
		}
	}
}
