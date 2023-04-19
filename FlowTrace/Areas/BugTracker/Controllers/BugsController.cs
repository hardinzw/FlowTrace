using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlowTrace.Areas.BugTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using FlowTrace.Areas.Identity.Data;
using FlowTrace.Areas.BugTracker.Data;

namespace FlowTrace.Areas.BugTracker.Controllers
{
    [Area("BugTracker")]
    public class BugsController : Controller
    {
        private readonly BugTrackerDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BugsController(BugTrackerDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: BugTracker/Bugs
        [Authorize]
        public async Task<IActionResult> Index(string searchString = "")
        {
            var bugs = from b in _context.Bugs
                       select b;

            if (!string.IsNullOrEmpty(searchString))
            {
                bugs = bugs.Where(b => b.Assignee == searchString || b.Reporter == searchString || b.Status == searchString || b.StatusCategory == searchString || b.Type == searchString);
            }

            return bugs != null ?
            View(await bugs.ToListAsync()) :
            Problem("Entity set 'BugTrackerDbContext.Bugs'  is null.");
        }

        // GET: BugTracker/Bugs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bugs == null)
            {
                return NotFound();
            }

            var bug = await _context.Bugs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        // GET: BugTracker/Bugs/Create
        [Authorize]
        public IActionResult Create()
        {
            return PartialView("Create");
        }

        // POST: BugTracker/Bugs/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Summary,Assignee,Reporter,Status,StatusCategory,Type,Description")] Bug bug)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bug);
        }

        // GET: BugTracker/Bugs/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bugs == null)
            {
                return NotFound();
            }

            var bug = await _context.Bugs.FindAsync(id);
            if (bug == null)
            {
                return NotFound();
            }
            return View(bug);
        }

        // POST: BugTracker/Bugs/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Summary,Assignee,Reporter,Status,StatusCategory,Type,Description")] Bug bug)
        {
            if (id != bug.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bug);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BugExists(bug.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bug);
        }

        // GET: BugTracker/Bugs/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bugs == null)
            {
                return NotFound();
            }

            var bug = await _context.Bugs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        // POST: BugTracker/Bugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bugs == null)
            {
                return Problem("Entity set 'BugTrackerDbContext.Bugs'  is null.");
            }
            var bug = await _context.Bugs.FindAsync(id);
            if (bug != null)
            {
                _context.Bugs.Remove(bug);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BugExists(int id)
        {
            return (_context.Bugs?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //private async string GetApplicationUserNames()
        //{
        //    var appUsers = _userManager.Users.ToListAsync();
        //    await 
        //}
    }
}
