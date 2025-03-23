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
    public class ConferencesController : Controller
    {
        private readonly DbconappContext _context;

        public ConferencesController(DbconappContext context)
        {
            _context = context;
        }

        // GET: Conferences
        public async Task<IActionResult> Index()
        {
            var dbconappContext = _context.Conferences.Include(c => c.Organizator);
            return View(await dbconappContext.ToListAsync());
        }

        // GET: Conferences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conference = await _context.Conferences
                .Include(c => c.Organizator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conference == null)
            {
                return NotFound();
            }

            return View(conference);
        }

        // GET: Conferences/Create
        public IActionResult Create()
        {
            //ViewData["OrganizatorId"] = new SelectList(_context.Organizators, "Id", "Description");
            ViewBag.PublicationId = new SelectList(_context.Publications.ToList(), "Id", "Title");
            ViewBag.OrganizatorId = new SelectList(_context.Organizators.ToList(), "Id", "Description");
            return View();
        }

        // POST: Conferences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Place,Date,PublicationId,OrganizatorId,Id")] Conference conference)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conference);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganizatorId"] = new SelectList(_context.Organizators, "Id", "Description", conference.OrganizatorId);
            ViewData["PublicationId"] = new SelectList(_context.Publications, "Id", "Title", conference.PublicationId);
            return View(conference);
        }

        // GET: Conferences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conference = await _context.Conferences.FindAsync(id);
            if (conference == null)
            {
                return NotFound();
            }
            ViewData["OrganizatorId"] = new SelectList(_context.Organizators, "Id", "Description", conference.OrganizatorId);
            return View(conference);
        }

        // POST: Conferences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,Date,PublicationId,OrganizatorId,Id")] Conference conference)
        {
            if (id != conference.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conference);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConferenceExists(conference.Id))
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
            ViewData["OrganizatorId"] = new SelectList(_context.Organizators, "Id", "Description", conference.OrganizatorId);
            return View(conference);
        }

        // GET: Conferences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conference = await _context.Conferences
                .Include(c => c.Organizator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conference == null)
            {
                return NotFound();
            }

            return View(conference);
        }

        // POST: Conferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conference = await _context.Conferences.FindAsync(id);
            if (conference != null)
            {
                _context.Conferences.Remove(conference);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConferenceExists(int id)
        {
            return _context.Conferences.Any(e => e.Id == id);
        }
    }
}
