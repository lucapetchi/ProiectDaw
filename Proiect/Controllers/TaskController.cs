using Microsoft.AspNetCore.Mvc;
using Proiect.Data;
using Task = Proiect.Models.Task;

namespace Proiect.Controllers
{
    public class TaskController : Controller
    {
        private readonly AppDbContext db;
		public TaskController(AppDbContext context) { db = context; }
		public IActionResult Index() {
            var Tasks = from Task in db.Tasks orderby Task.Name select Task;
            ViewBag.Tasks = Tasks;
            return View();
        }

        
        public IActionResult Add()
            {
                return View();
            }
            [HttpPost]
            public IActionResult Add(Task task) { 
                try
                {
                    db.Tasks.Add(task);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return View();
                }
            }
        
       
    }
}
