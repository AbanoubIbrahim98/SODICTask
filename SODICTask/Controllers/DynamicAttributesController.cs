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
    public class DynamicAttributesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DynamicAttributesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DynamicAttributes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DynamicAttributes.Include(d => d.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DynamicAttributes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DynamicAttributes == null)
            {
                return NotFound();
            }

            var dynamicAttribute = await _context.DynamicAttributes
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.DynamicAttributeId == id);
            if (dynamicAttribute == null)
            {
                return NotFound();
            }

            return View(dynamicAttribute);
        }

        // GET: DynamicAttributes/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: DynamicAttributes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DynamicAttributeId,CategoryId,Name,AttributeType,IsMultipleSelect")] DynamicAttribute dynamicAttribute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dynamicAttribute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", dynamicAttribute.CategoryId);
            return View(dynamicAttribute);
        }

        // GET: DynamicAttributes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DynamicAttributes == null)
            {
                return NotFound();
            }

            var dynamicAttribute = await _context.DynamicAttributes.FindAsync(id);
            if (dynamicAttribute == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", dynamicAttribute.CategoryId);
            return View(dynamicAttribute);
        }

        // POST: DynamicAttributes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DynamicAttributeId,CategoryId,Name,AttributeType,IsMultipleSelect")] DynamicAttribute dynamicAttribute)
        {
            if (id != dynamicAttribute.DynamicAttributeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dynamicAttribute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DynamicAttributeExists(dynamicAttribute.DynamicAttributeId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", dynamicAttribute.CategoryId);
            return View(dynamicAttribute);
        }

        // GET: DynamicAttributes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DynamicAttributes == null)
            {
                return NotFound();
            }

            var dynamicAttribute = await _context.DynamicAttributes
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.DynamicAttributeId == id);
            if (dynamicAttribute == null)
            {
                return NotFound();
            }

            return View(dynamicAttribute);
        }

        // POST: DynamicAttributes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DynamicAttributes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DynamicAttributes'  is null.");
            }
            var dynamicAttribute = await _context.DynamicAttributes.FindAsync(id);
            if (dynamicAttribute != null)
            {
                _context.DynamicAttributes.Remove(dynamicAttribute);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DynamicAttributeExists(int id)
        {
          return _context.DynamicAttributes.Any(e => e.DynamicAttributeId == id);
        }
    }
}
