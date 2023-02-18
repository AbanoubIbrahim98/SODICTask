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
    public class AttributeValuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttributeValuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AttributeValues
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AttributeValues.Include(a => a.DynamicAttribute).Include(a => a.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AttributeValues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AttributeValues == null)
            {
                return NotFound();
            }

            var attributeValue = await _context.AttributeValues
                .Include(a => a.DynamicAttribute)
                .Include(a => a.Product)
                .FirstOrDefaultAsync(m => m.AttributeValueId == id);
            if (attributeValue == null)
            {
                return NotFound();
            }

            return View(attributeValue);
        }

        // GET: AttributeValues/Create
        public IActionResult Create()
        {
            ViewData["DynamicAttributeId"] = new SelectList(_context.DynamicAttributes, "DynamicAttributeId", "DynamicAttributeId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: AttributeValues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttributeValueId,ProductId,DynamicAttributeId,Value")] AttributeValue attributeValue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attributeValue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DynamicAttributeId"] = new SelectList(_context.DynamicAttributes, "DynamicAttributeId", "DynamicAttributeId", attributeValue.DynamicAttributeId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", attributeValue.ProductId);
            return View(attributeValue);
        }

        // GET: AttributeValues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AttributeValues == null)
            {
                return NotFound();
            }

            var attributeValue = await _context.AttributeValues.FindAsync(id);
            if (attributeValue == null)
            {
                return NotFound();
            }
            ViewData["DynamicAttributeId"] = new SelectList(_context.DynamicAttributes, "DynamicAttributeId", "DynamicAttributeId", attributeValue.DynamicAttributeId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", attributeValue.ProductId);
            return View(attributeValue);
        }

        // POST: AttributeValues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AttributeValueId,ProductId,DynamicAttributeId,Value")] AttributeValue attributeValue)
        {
            if (id != attributeValue.AttributeValueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attributeValue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttributeValueExists(attributeValue.AttributeValueId))
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
            ViewData["DynamicAttributeId"] = new SelectList(_context.DynamicAttributes, "DynamicAttributeId", "DynamicAttributeId", attributeValue.DynamicAttributeId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", attributeValue.ProductId);
            return View(attributeValue);
        }

        // GET: AttributeValues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AttributeValues == null)
            {
                return NotFound();
            }

            var attributeValue = await _context.AttributeValues
                .Include(a => a.DynamicAttribute)
                .Include(a => a.Product)
                .FirstOrDefaultAsync(m => m.AttributeValueId == id);
            if (attributeValue == null)
            {
                return NotFound();
            }

            return View(attributeValue);
        }

        // POST: AttributeValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AttributeValues == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AttributeValues'  is null.");
            }
            var attributeValue = await _context.AttributeValues.FindAsync(id);
            if (attributeValue != null)
            {
                _context.AttributeValues.Remove(attributeValue);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttributeValueExists(int id)
        {
          return _context.AttributeValues.Any(e => e.AttributeValueId == id);
        }
    }
}
