using CPW219_CRUD_Troubleshooting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CPW219_CRUD_Troubleshooting.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext context;

        public StudentsController(SchoolContext dbContext)
        {
            context = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Student> students = await (from student in context.Students
                                            select student).ToListAsync();
            return View(students);
        }
       // [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student p)
        {
            if (!ModelState.IsValid)
            {
                context.Students.Add(p);
                await context.Students.AddAsync(p);
                context.SaveChanges();
                return View();
            }
            //Show web page with errors
            return View(p);
        }

        public async Task<IActionResult> Edit(int id)
        {
            //get the product by id
            var student = await context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            //show it on web page
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student p)
        {
            if (ModelState.IsValid)
            {
                context.Update(p);
                await context.Students.AddAsync(p);
                context.SaveChanges();
                ViewData["Message"] = "Product Updated!";
                return RedirectToAction("Index");
            }
            //return view with errors
            return View(p);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await context.Students.FindAsync(id);

            if(student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {

            var student = await context.Students.FindAsync(id);
            context.Students.Remove(student);
            context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
