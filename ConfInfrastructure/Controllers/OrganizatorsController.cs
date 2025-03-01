using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConfDomain.Model;
using ConfInfrastructure;

namespace ConfInfrastructure.Controllers
{
    public class OrganizatorsController : Controller
    {
        private readonly DbconappContext _context;

        public OrganizatorsController(DbconappContext context)
        {
            _context = context;
        }

        // GET: Organizators
        public async Task<IActionResult> Index()
        {
            return View(await _context.Organizators.ToListAsync());
        }

        // GET: Organizators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizator = await _context.Organizators
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organizator == null)
            {
                return NotFound();
            }

            return View(organizator);
        }

        // GET: Organizators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Organizators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Party,Id")] Organizator organizator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organizator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organizator);
        }

        // GET: Organizators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizator = await _context.Organizators.FindAsync(id);
            if (organizator == null)
            {
                return NotFound();
            }
            return View(organizator);
        }

        // POST: Organizators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Party,Id")] Organizator organizator)
        {
            if (id != organizator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organizator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizatorExists(organizator.Id))
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
            return View(organizator);
        }

        // GET: Organizators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizator = await _context.Organizators
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organizator == null)
            {
                return NotFound();
            }

            return View(organizator);
        }

        // POST: Organizators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var organizator = await _context.Organizators.FindAsync(id);
            if (organizator != null)
            {
                _context.Organizators.Remove(organizator);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizatorExists(int id)
        {
            return _context.Organizators.Any(e => e.Id == id);
        }
    }
}
