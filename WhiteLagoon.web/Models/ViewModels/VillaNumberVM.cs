using Microsoft.AspNetCore.Mvc.Rendering;
using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.web.Models.ViewModels
{
	public class VillaNumberVM
	{
		public VillaNumber? VillaNumber { get; set; }
		[Microsoft.AspNetCore.Mvc.ModelBinding.Validation.ValidateNever]
		public IEnumerable<SelectListItem>? VillaList {  get; set; }
	}
}
