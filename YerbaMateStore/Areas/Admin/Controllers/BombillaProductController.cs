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
    public class BombillaProductController : Controller
    {
        private readonly AppDbContext _context;

        public BombillaProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/BombillaProduct
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.BombillaProducts.Include(b => b.Country);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/BombillaProduct/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BombillaProducts == null)
            {
                return NotFound();
            }

            var bombillaProduct = await _context.BombillaProducts
                .Include(b => b.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bombillaProduct == null)
            {
                return NotFound();
            }

            return View(bombillaProduct);
        }

        // GET: Admin/BombillaProduct/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            return View();
        }

        // POST: Admin/BombillaProduct/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Length,Material,Description,IsUnscrewed,Price,DiscountPrice,CountryId")] BombillaProduct bombillaProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bombillaProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", bombillaProduct.CountryId);
            return View(bombillaProduct);
        }

        // GET: Admin/BombillaProduct/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BombillaProducts == null)
            {
                return NotFound();
            }

            var bombillaProduct = await _context.BombillaProducts.FindAsync(id);
            if (bombillaProduct == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", bombillaProduct.CountryId);
            return View(bombillaProduct);
        }

        // POST: Admin/BombillaProduct/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Length,Material,Description,IsUnscrewed,Price,DiscountPrice,CountryId")] BombillaProduct bombillaProduct)
        {
            if (id != bombillaProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bombillaProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BombillaProductExists(bombillaProduct.Id))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", bombillaProduct.CountryId);
            return View(bombillaProduct);
        }

        // GET: Admin/BombillaProduct/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BombillaProducts == null)
            {
                return NotFound();
            }

            var bombillaProduct = await _context.BombillaProducts
                .Include(b => b.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bombillaProduct == null)
            {
                return NotFound();
            }

            return View(bombillaProduct);
        }

        // POST: Admin/BombillaProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BombillaProducts == null)
            {
                return Problem("Entity set 'AppDbContext.BombillaProducts'  is null.");
            }
            var bombillaProduct = await _context.BombillaProducts.FindAsync(id);
            if (bombillaProduct != null)
            {
                _context.BombillaProducts.Remove(bombillaProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BombillaProductExists(int id)
        {
          return (_context.BombillaProducts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
