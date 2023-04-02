using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YerbaMateStore.Models.DataAccess;
using YerbaMateStore.Models.Entities;

namespace YerbaMateStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CupProductController : Controller
    {
        private readonly AppDbContext _context;

        public CupProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CupProduct
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CupProducts.Include(c => c.Country);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/CupProduct/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CupProducts == null)
            {
                return NotFound();
            }

            var cupProduct = await _context.CupProducts
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cupProduct == null)
            {
                return NotFound();
            }

            return View(cupProduct);
        }

        // GET: Admin/CupProduct/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            return View();
        }

        // POST: Admin/CupProduct/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Volume,Material,Description,Price,DiscountPrice,CountryId")] CupProduct cupProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cupProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", cupProduct.CountryId);
            return View(cupProduct);
        }

        // GET: Admin/CupProduct/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CupProducts == null)
            {
                return NotFound();
            }

            var cupProduct = await _context.CupProducts.FindAsync(id);
            if (cupProduct == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", cupProduct.CountryId);
            return View(cupProduct);
        }

        // POST: Admin/CupProduct/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Volume,Material,Description,Price,DiscountPrice,CountryId")] CupProduct cupProduct)
        {
            if (id != cupProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cupProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CupProductExists(cupProduct.Id))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", cupProduct.CountryId);
            return View(cupProduct);
        }

        // GET: Admin/CupProduct/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CupProducts == null)
            {
                return NotFound();
            }

            var cupProduct = await _context.CupProducts
                .Include(c => c.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cupProduct == null)
            {
                return NotFound();
            }

            return View(cupProduct);
        }

        // POST: Admin/CupProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CupProducts == null)
            {
                return Problem("Entity set 'AppDbContext.CupProducts'  is null.");
            }
            var cupProduct = await _context.CupProducts.FindAsync(id);
            if (cupProduct != null)
            {
                _context.CupProducts.Remove(cupProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CupProductExists(int id)
        {
          return (_context.CupProducts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
