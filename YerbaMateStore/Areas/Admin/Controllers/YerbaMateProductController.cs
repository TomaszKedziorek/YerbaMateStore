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
  public class YerbaMateProductController : Controller
  {
    private readonly AppDbContext _context;

    public YerbaMateProductController(AppDbContext context)
    {
      _context = context;
    }

    // GET: Admin/YerbaMateProduct
    public async Task<IActionResult> Index()
    {
      var appDbContext = _context.YerbaMateProducts.Include(y => y.Country);
      return View(await appDbContext.ToListAsync());
    }

    // GET: Admin/YerbaMateProduct/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null || _context.YerbaMateProducts == null)
      {
        return NotFound();
      }

      var yerbaMateProduct = await _context.YerbaMateProducts
          .Include(y => y.Country)
          .FirstOrDefaultAsync(m => m.Id == id);
      if (yerbaMateProduct == null)
      {
        return NotFound();
      }

      return View(yerbaMateProduct);
    }

    // GET: Admin/YerbaMateProduct/Create
    public IActionResult Create()
    {
      ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
      return View();
    }

    // POST: Admin/YerbaMateProduct/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Brand,Description,Composition,HasAdditives,Weight,Price,DiscountPrice,CountryId")] YerbaMateProduct yerbaMateProduct)
    {
      if (ModelState.IsValid)
      {
        _context.Add(yerbaMateProduct);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();
      Console.WriteLine("-----------------------------------------");
      foreach (var error in errors)
      {
        Console.WriteLine(error);
      }
      ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", yerbaMateProduct.CountryId);
      return View(yerbaMateProduct);
    }

    // GET: Admin/YerbaMateProduct/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null || _context.YerbaMateProducts == null)
      {
        return NotFound();
      }

      var yerbaMateProduct = await _context.YerbaMateProducts.FindAsync(id);
      if (yerbaMateProduct == null)
      {
        return NotFound();
      }
      ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", yerbaMateProduct.CountryId);
      return View(yerbaMateProduct);
    }

    // POST: Admin/YerbaMateProduct/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Brand,Description,Composition,HasAdditives,Weight,Price,DiscountPrice,CountryId")] YerbaMateProduct yerbaMateProduct)
    {
      if (id != yerbaMateProduct.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(yerbaMateProduct);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!YerbaMateProductExists(yerbaMateProduct.Id))
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
      ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", yerbaMateProduct.CountryId);
      return View(yerbaMateProduct);
    }

    // GET: Admin/YerbaMateProduct/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null || _context.YerbaMateProducts == null)
      {
        return NotFound();
      }

      var yerbaMateProduct = await _context.YerbaMateProducts
          .Include(y => y.Country)
          .FirstOrDefaultAsync(m => m.Id == id);
      if (yerbaMateProduct == null)
      {
        return NotFound();
      }

      return View(yerbaMateProduct);
    }

    // POST: Admin/YerbaMateProduct/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      if (_context.YerbaMateProducts == null)
      {
        return Problem("Entity set 'AppDbContext.YerbaMateProducts'  is null.");
      }
      var yerbaMateProduct = await _context.YerbaMateProducts.FindAsync(id);
      if (yerbaMateProduct != null)
      {
        _context.YerbaMateProducts.Remove(yerbaMateProduct);
      }

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool YerbaMateProductExists(int id)
    {
      return (_context.YerbaMateProducts?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}
