using Microsoft.AspNetCore.Mvc;

namespace Proiect.Controllers
{
	public class ProjectsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
