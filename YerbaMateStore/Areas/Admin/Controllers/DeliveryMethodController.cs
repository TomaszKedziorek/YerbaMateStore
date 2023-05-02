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
    public class DeliveryMethodController : Controller
    {
        private readonly AppDbContext _context;

        public DeliveryMethodController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/DeliveryMethod
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DeliveryMethod.Include(d => d.PaymentMethod);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/DeliveryMethod/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DeliveryMethod == null)
            {
                return NotFound();
            }

            var deliveryMethod = await _context.DeliveryMethod
                .Include(d => d.PaymentMethod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryMethod == null)
            {
                return NotFound();
            }

            return View(deliveryMethod);
        }

        // GET: Admin/DeliveryMethod/Create
        public IActionResult Create()
        {
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "Id", "Name");
            return View();
        }

        // POST: Admin/DeliveryMethod/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Carrier,CollectionPoint,DeliveryTime,PaymentMethodId,Cost")] DeliveryMethod deliveryMethod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "Id", "Name", deliveryMethod.PaymentMethodId);
            return View(deliveryMethod);
        }

        // GET: Admin/DeliveryMethod/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DeliveryMethod == null)
            {
                return NotFound();
            }

            var deliveryMethod = await _context.DeliveryMethod.FindAsync(id);
            if (deliveryMethod == null)
            {
                return NotFound();
            }
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "Id", "Name", deliveryMethod.PaymentMethodId);
            return View(deliveryMethod);
        }

        // POST: Admin/DeliveryMethod/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Carrier,CollectionPoint,DeliveryTime,PaymentMethodId,Cost")] DeliveryMethod deliveryMethod)
        {
            if (id != deliveryMethod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryMethod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryMethodExists(deliveryMethod.Id))
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
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "Id", "Name", deliveryMethod.PaymentMethodId);
            return View(deliveryMethod);
        }

        // GET: Admin/DeliveryMethod/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DeliveryMethod == null)
            {
                return NotFound();
            }

            var deliveryMethod = await _context.DeliveryMethod
                .Include(d => d.PaymentMethod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryMethod == null)
            {
                return NotFound();
            }

            return View(deliveryMethod);
        }

        // POST: Admin/DeliveryMethod/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DeliveryMethod == null)
            {
                return Problem("Entity set 'AppDbContext.DeliveryMethod'  is null.");
            }
            var deliveryMethod = await _context.DeliveryMethod.FindAsync(id);
            if (deliveryMethod != null)
            {
                _context.DeliveryMethod.Remove(deliveryMethod);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryMethodExists(int id)
        {
          return (_context.DeliveryMethod?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
