using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bugLog.Models;

namespace bugLog.Controllers
{
    public class BugsController : Controller
    {
        private readonly BugDbContext _context;

        public BugsController(BugDbContext context)
        {
            _context = context;
        }

        // GET: Bugs
        public async Task<IActionResult> Index()
        {
            var bugDbContext = _context.Bugs.Include(b => b.BugAssignedToNavigation).Include(b => b.BugClosedByNavigation).Include(b => b.BugCreatedByNavigation);
            return View(await bugDbContext.ToListAsync());
        }

        // GET: Bugs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bugs
                .Include(b => b.BugAssignedToNavigation)
                .Include(b => b.BugClosedByNavigation)
                .Include(b => b.BugCreatedByNavigation)
                .FirstOrDefaultAsync(m => m.BugId == id);
            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        // GET: Bugs/Create
        public IActionResult Create()
        {
            ViewData["BugAssignedTo"] = new SelectList(_context.UserProfiles, "UserId", "DisplayName");
            ViewData["BugClosedBy"] = new SelectList(_context.UserProfiles, "UserId", "DisplayName");
            ViewData["BugCreatedBy"] = new SelectList(_context.UserProfiles, "UserId", "DisplayName");
            return View();
        }

        // POST: Bugs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BugId,AssignedToTeamId,BugReportedBy,BugTitle,BugDescription,BugAssignedTo,BugStatus,BugSeverity,BugCreatedBy,BugCreatedOn,BugClosedBy,BugClosedOn,BugResolutionSummary")] Bug bug)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BugAssignedTo"] = new SelectList(_context.UserProfiles, "UserId", "DisplayName", bug.BugAssignedTo);
            ViewData["BugClosedBy"] = new SelectList(_context.UserProfiles, "UserId", "DisplayName", bug.BugClosedBy);
            ViewData["BugCreatedBy"] = new SelectList(_context.UserProfiles, "UserId", "DisplayName", bug.BugCreatedBy);
            return View(bug);
        }

        // GET: Bugs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bugs.FindAsync(id);
            if (bug == null)
            {
                return NotFound();
            }
            ViewData["BugAssignedTo"] = new SelectList(_context.UserProfiles, "UserId", "DisplayName", bug.BugAssignedTo);
            ViewData["BugClosedBy"] = new SelectList(_context.UserProfiles, "UserId", "DisplayName", bug.BugClosedBy);
            ViewData["BugCreatedBy"] = new SelectList(_context.UserProfiles, "UserId", "DisplayName", bug.BugCreatedBy);
            return View(bug);
        }

        // POST: Bugs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BugId,AssignedToTeamId,BugReportedBy,BugTitle,BugDescription,BugAssignedTo,BugStatus,BugSeverity,BugCreatedBy,BugCreatedOn,BugClosedBy,BugClosedOn,BugResolutionSummary")] Bug bug)
        {
            if (id != bug.BugId)
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
                    if (!BugExists(bug.BugId))
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
            ViewData["BugAssignedTo"] = new SelectList(_context.UserProfiles, "UserId", "DisplayName", bug.BugAssignedTo);
            ViewData["BugClosedBy"] = new SelectList(_context.UserProfiles, "UserId", "DisplayName", bug.BugClosedBy);
            ViewData["BugCreatedBy"] = new SelectList(_context.UserProfiles, "UserId", "DisplayName", bug.BugCreatedBy);
            return View(bug);
        }

        // GET: Bugs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bugs
                .Include(b => b.BugAssignedToNavigation)
                .Include(b => b.BugClosedByNavigation)
                .Include(b => b.BugCreatedByNavigation)
                .FirstOrDefaultAsync(m => m.BugId == id);
            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        // POST: Bugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bug = await _context.Bugs.FindAsync(id);
            _context.Bugs.Remove(bug);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BugExists(int id)
        {
            return _context.Bugs.Any(e => e.BugId == id);
        }
    }
}
