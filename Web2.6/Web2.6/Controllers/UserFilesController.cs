using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web2._6.Data;
using Web2._6.Models;

namespace Web2._6.Controllers
{
    public class UserFilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserFilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserFiles
        public async Task<IActionResult> Index()
        {
              return _context.AttachedFiles != null ? 
                          View(await _context.AttachedFiles.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AttachedFiles'  is null.");
        }

        // GET: UserFiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AttachedFiles == null)
            {
                return NotFound();
            }

            var userFile = await _context.AttachedFiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userFile == null)
            {
                return NotFound();
            }

            return View(userFile);
        }

        // GET: UserFiles/Create
        public IActionResult Create(int id)
        {
            ViewBag.ReplyId = id;
            return View();
        }

        // POST: UserFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Path,ParentReplyId")] UserFile userFile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userFile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userFile);
        }

        // GET: UserFiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AttachedFiles == null)
            {
                return NotFound();
            }

            var userFile = await _context.AttachedFiles.FindAsync(id);
            if (userFile == null)
            {
                return NotFound();
            }
            return View(userFile);
        }

        // POST: UserFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Path,ParentReplyId")] UserFile userFile)
        {
            if (id != userFile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserFileExists(userFile.Id))
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
            return View(userFile);
        }

        // GET: UserFiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AttachedFiles == null)
            {
                return NotFound();
            }

            var userFile = await _context.AttachedFiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userFile == null)
            {
                return NotFound();
            }

            return View(userFile);
        }

        // POST: UserFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AttachedFiles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AttachedFiles'  is null.");
            }
            var userFile = await _context.AttachedFiles.FindAsync(id);
            if (userFile != null)
            {
                _context.AttachedFiles.Remove(userFile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserFileExists(int id)
        {
          return (_context.AttachedFiles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
