using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web2._6.Data;
using Web2._6.Models;

namespace Web2._6.Controllers
{
    public class ForumCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ForumCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ForumCategories
        public async Task<IActionResult> Index()
        {
              return _context.Forums != null ? 
                          View(await _context.Forums.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Forums'  is null.");
        }

        // GET: ForumCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Forums == null)
            {
                return NotFound();
            }

            var forumCategory = await _context.Forums
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumCategory == null)
            {
                return NotFound();
            }

            return View(forumCategory);
        }

        // GET: ForumCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ForumCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ForumCategory forumCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(forumCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(forumCategory);
        }

        // GET: ForumCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Forums == null)
            {
                return NotFound();
            }

            var forumCategory = await _context.Forums.FindAsync(id);
            if (forumCategory == null)
            {
                return NotFound();
            }
            return View(forumCategory);
        }

        // POST: ForumCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ForumCategory forumCategory)
        {
            if (id != forumCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forumCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumCategoryExists(forumCategory.Id))
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
            return View(forumCategory);
        }

        // GET: ForumCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Forums == null)
            {
                return NotFound();
            }

            var forumCategory = await _context.Forums
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumCategory == null)
            {
                return NotFound();
            }

            return View(forumCategory);
        }

        // POST: ForumCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Forums == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Forums'  is null.");
            }
            var forumCategory = await _context.Forums.FindAsync(id);
            if (forumCategory != null)
            {
                _context.Forums.Remove(forumCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumCategoryExists(int id)
        {
          return (_context.Forums?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
