using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Organizer.Data;
using Organizer.Models;

namespace OrganizerCore.Controllers
{
    public class TasksController : Controller
    {
        private readonly TaskContext _context;

        public TasksController(TaskContext context)
        {
            _context = context;    
        }

        // GET: Tasks
        public async Task<IActionResult> Index(string sortOrder, string searchString, string searchString2)
        {
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["GroupSortParm"] = sortOrder == "Group" ? "group_desc" : "Group";
            ViewData["PrioritySortParm"] = sortOrder == "Priority" ? "priority_desc" : "Priority";

            ViewData["CurrentFilter"] = searchString;

            var tasks = from t in _context.Tasks
                           select t;

            if (!String.IsNullOrEmpty(searchString))
            {                
                tasks = tasks.Where(t => t.Name.Contains(searchString)
                                       || t.Group.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(searchString2))
            {
                DateTime par = DateTime.Parse(searchString2);
                tasks = tasks.Where(t => t.Date.Equals(par));
            }

            switch (sortOrder)
            {
                case "group_desc":
                    tasks = tasks.OrderByDescending(t => t.Group);
                    break;
                case "Group":
                    tasks = tasks.OrderBy(t => t.Group);
                    break;
                case "priority_desc":
                    tasks = tasks.OrderByDescending(t => t.Priority);
                    break;
                case "Priority":
                    tasks = tasks.OrderBy(t => t.Priority);
                    break;
                case "name_desc":
                    tasks = tasks.OrderByDescending(t => t.Name);
                    break;
                case "Name":
                    tasks = tasks.OrderBy(t => t.Name);
                    break;
                case "date_desc":
                    tasks = tasks.OrderByDescending(t => t.Date);
                    break;
                case "Date":
                    tasks = tasks.OrderBy(t => t.Date);
                    break;
                default:
                    tasks = tasks.OrderBy(t => t.Date);
                    break;
            }
            return View(await tasks.AsNoTracking().ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           // var task = await _context.Tasks
                //.SingleOrDefaultAsync(m => m.ID == id);

            var task = await _context.Tasks
                .Include(s => s.Subtasks)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Group,Priority,Date")] Organizer.Models.Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(task);
        }
        

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.SingleOrDefaultAsync(m => m.ID == id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Group,Priority,Date")] Organizer.Models.Task task)
        {
            if (id != task.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(task);
        }
        

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .SingleOrDefaultAsync(m => m.ID == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }
        

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.SingleOrDefaultAsync(m => m.ID == id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.ID == id);
        }

    }
}
