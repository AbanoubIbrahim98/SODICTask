using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SODICTask.Data;
using SODICTask.Models;

namespace SODICTask.Controllers
{
    public class FileProdsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FileProdsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FileProds
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FileProds.Include(f => f.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FileProds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FileProds == null)
            {
                return NotFound();
            }

            var fileProd = await _context.FileProds
                .Include(f => f.Product)
                .FirstOrDefaultAsync(m => m.FileId == id);
            if (fileProd == null)
            {
                return NotFound();
            }

            return View(fileProd);
        }

        // GET: FileProds/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: FileProds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FileId,ProductId,FileName,FileType,FileData")] FileProd fileProd)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fileProd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", fileProd.ProductId);
            return View(fileProd);
        }

        // GET: FileProds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FileProds == null)
            {
                return NotFound();
            }

            var fileProd = await _context.FileProds.FindAsync(id);
            if (fileProd == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", fileProd.ProductId);
            return View(fileProd);
        }

        // POST: FileProds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FileId,ProductId,FileName,FileType,FileData")] FileProd fileProd)
        {
            if (id != fileProd.FileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileProd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileProdExists(fileProd.FileId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", fileProd.ProductId);
            return View(fileProd);
        }

        // GET: FileProds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FileProds == null)
            {
                return NotFound();
            }

            var fileProd = await _context.FileProds
                .Include(f => f.Product)
                .FirstOrDefaultAsync(m => m.FileId == id);
            if (fileProd == null)
            {
                return NotFound();
            }

            return View(fileProd);
        }

        // POST: FileProds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FileProds == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FileProds'  is null.");
            }
            var fileProd = await _context.FileProds.FindAsync(id);
            if (fileProd != null)
            {
                _context.FileProds.Remove(fileProd);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileProdExists(int id)
        {
          return _context.FileProds.Any(e => e.FileId == id);
        }
    }
}
