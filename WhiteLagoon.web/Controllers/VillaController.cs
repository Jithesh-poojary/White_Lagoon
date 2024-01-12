using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var villas=_db.Villas.ToList();
            return View(villas) ;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
		public IActionResult Create(Villa villa)
		{
            if (villa.Name == villa.Description)
            {
                ModelState.AddModelError("name", "The Name and Description may not be the same");
            }
            if (ModelState.IsValid)
            {
                _db.Villas.Add(villa);
                _db.SaveChanges();
                //return RedirectToAction("Index","Villa");     //can also be used controller as a parameter just to identify to which controller it belongs
                return RedirectToAction("Index");
            }
            return View(villa);
		}

        public IActionResult Update(int villaId)
        {
            Villa? villa = _db.Villas.FirstOrDefault(x => x.Id == villaId);
            if (villa == null)
            {
                return RedirectToAction("Error","Home");
            }
            return View(villa) ;
        }

		[HttpPost]
		public IActionResult Update(Villa villa)
		{
			
			if (ModelState.IsValid && villa.Id>0)
			{
				_db.Villas.Update(villa);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(villa);
		}

		public IActionResult Delete(int villaId)
		{
			Villa? villa = _db.Villas.FirstOrDefault(x => x.Id == villaId);
			if (villa is null)
			{
				return RedirectToAction("Error", "Home");
			}
			return View(villa);
		}

        [HttpPost]
        public IActionResult Delete(Villa villa)
        {
            Villa? objFromDb= _db.Villas.FirstOrDefault(u=>u.Id==villa.Id);
            if (objFromDb is not null)
            {
                _db.Villas.Remove(objFromDb);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(villa);
        }
    }
}
