using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
			var VillaNumbers=_context.VillaNumbers.Include(u=>u.Villa).ToList();
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
		public IActionResult Create(VillaNumberVM obj)
		{
			//ModelState.Remove("Villa");

			bool roomNumberExists=_context.VillaNumbers.Any(u=>u.Villa_Number==obj.VillaNumber.Villa_Number);

			if (ModelState.IsValid && !roomNumberExists)
			{
				_context.VillaNumbers.Add(obj.VillaNumber);
				_context.SaveChanges();
				TempData["success"] = "The Villa Number is Created Successfully..";
				//return RedirectToAction("Index","Villa");     //can also be used controller as a parameter just to identify to which controller it belongs
				return RedirectToAction(nameof(Index));
			}
			obj.VillaList = _context.Villas.ToList().Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id.ToString()
			});
			if (roomNumberExists)
			{
				TempData["error"] = "The Villa number already exists";
			}
			return View(obj);
		}

		public IActionResult Update(int villaNumberId)
		{
			VillaNumberVM villaNumberVM = new()
			{
				VillaList = _context.Villas.ToList().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				VillaNumber=_context.VillaNumbers.FirstOrDefault(u=>u.Villa_Number==villaNumberId)
			};
			if (villaNumberVM.VillaNumber == null)
			{
				return RedirectToAction("Error", "Home");
			}
			return View(villaNumberVM);
		}

		[HttpPost]
		public IActionResult Update(VillaNumberVM villaNumberVM)
		{


			if (ModelState.IsValid)
			{
				_context.VillaNumbers.Update(villaNumberVM.VillaNumber);
				_context.SaveChanges();
				TempData["success"] = "The Villa Number is Updated Successfully..";
				//return RedirectToAction("Index","Villa");     //can also be used controller as a parameter just to identify to which controller it belongs
				return RedirectToAction(nameof(Index));
			}
			villaNumberVM.VillaList = _context.Villas.ToList().Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id.ToString()
			});
			
			return View(villaNumberVM);
		}

		public IActionResult Delete(int villaNumberId)
		{
			VillaNumberVM villaNumberVM = new()
			{
				VillaList = _context.Villas.ToList().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				VillaNumber = _context.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId)
			};
			if (villaNumberVM.VillaNumber == null)
			{
				return RedirectToAction("Error", "Home");
			}
			return View(villaNumberVM);
		}


		[HttpPost]
		public IActionResult Delete(VillaNumberVM villaNumberVM)
		{
			VillaNumber? objFromDb = _context.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);
			if (objFromDb is not null)
			{
				_context.VillaNumbers.Remove(objFromDb);
				_context.SaveChanges();
				TempData["success"] = "The Villa Number has been Deleted Successfully..";
				return RedirectToAction(nameof(Index));
			}
			TempData["error"] = "The Villa Number could not be deleted..";
			return View();
		}

	}
}
