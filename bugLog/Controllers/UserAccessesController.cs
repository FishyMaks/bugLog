using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bugLog.Models;

namespace bugLog.Controllers
{
    public class UserAccessesController : Controller
    {
        private readonly BugDbContext _context;

        public UserAccessesController(BugDbContext context)
        {
            _context = context;
        }

        // GET: UserAccesses
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserAccesses.ToListAsync());
        }

        // GET: UserAccesses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccess = await _context.UserAccesses
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (userAccess == null)
            {
                return NotFound();
            }

            return View(userAccess);
        }

        // GET: UserAccesses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserAccesses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,UniqueKey,FromDate,ToDate")] UserAccess userAccess)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userAccess);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userAccess);
        }

        // GET: UserAccesses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccess = await _context.UserAccesses.FindAsync(id);
            if (userAccess == null)
            {
                return NotFound();
            }
            return View(userAccess);
        }

        // POST: UserAccesses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamId,UniqueKey,FromDate,ToDate")] UserAccess userAccess)
        {
            if (id != userAccess.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAccess);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAccessExists(userAccess.TeamId))
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
            return View(userAccess);
        }

        // GET: UserAccesses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccess = await _context.UserAccesses
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (userAccess == null)
            {
                return NotFound();
            }

            return View(userAccess);
        }

        // POST: UserAccesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAccess = await _context.UserAccesses.FindAsync(id);
            _context.UserAccesses.Remove(userAccess);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAccessExists(int id)
        {
            return _context.UserAccesses.Any(e => e.TeamId == id);
        }
    }
}
