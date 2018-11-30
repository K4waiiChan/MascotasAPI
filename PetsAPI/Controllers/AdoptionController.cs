using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetsAPI.Database;

namespace PetsAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdoptionController : ControllerBase
	{
		private readonly MascotasDbContext _context;
		public AdoptionController(MascotasDbContext context){
			_context = context;
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetAdoption() {

		}
	}
}