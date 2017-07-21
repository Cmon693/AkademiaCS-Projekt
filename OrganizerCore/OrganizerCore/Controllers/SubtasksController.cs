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
    public class SubtasksController : Controller
    {
        private readonly TaskContext _context;

        public SubtasksController(TaskContext context)
        {
            _context = context;    
        }

        // GET: Subtasks
        public async Task<IActionResult> Index()
        {
            var taskContext = _context.Subtasks.Include(s => s.Task);
            return View(await taskContext.ToListAsync());
        }

        // GET: Subtasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subtask = await _context.Subtasks
                .Include(s => s.Task)
                .SingleOrDefaultAsync(m => m.SubtaskID == id);
            if (subtask == null)
            {
                return NotFound();
            }

            return View(subtask);
        }

        // GET: Subtasks/Create
        public IActionResult Create()
        {
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "ID");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubtaskID,TaskID,Name,Group,Priority,Date")] Subtask subtask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subtask);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "ID", subtask.TaskID);
            return View(subtask);
        }

        // GET: Subtasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subtask = await _context.Subtasks.SingleOrDefaultAsync(m => m.SubtaskID == id);
            if (subtask == null)
            {
                return NotFound();
            }
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "ID", subtask.TaskID);
            return View(subtask);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubtaskID,TaskID,Name,Group,Priority,Date")] Subtask subtask)
        {
            if (id != subtask.SubtaskID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subtask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubtaskExists(subtask.SubtaskID))
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
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "ID", subtask.TaskID);
            return View(subtask);
        }

        // GET: Subtasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subtask = await _context.Subtasks
                .Include(s => s.Task)
                .SingleOrDefaultAsync(m => m.SubtaskID == id);
            if (subtask == null)
            {
                return NotFound();
            }

            return View(subtask);
        }

        // POST: Subtasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subtask = await _context.Subtasks.SingleOrDefaultAsync(m => m.SubtaskID == id);
            _context.Subtasks.Remove(subtask);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SubtaskExists(int id)
        {
            return _context.Subtasks.Any(e => e.SubtaskID == id);
        }
    }
}
