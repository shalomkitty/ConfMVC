using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConfDomain.Model;
using ConfInfrastructure;
using Microsoft.Extensions.Hosting;
using ClosedXML.Excel;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;

namespace ConfInfrastructure.Controllers
{
    public class ConferencesController : Controller
    {
        private readonly DbconappContext _context;

        public ConferencesController(DbconappContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ExportToExcel()
        {
            var products = await _context.Conferences
                .Select(p => new
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Place = p.Place,
                    Date = p.Date,
                    Price = p.Price
                })
                .ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Conferences");

                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Title";
                worksheet.Cell(1, 3).Value = "Description";
                worksheet.Cell(1, 4).Value = "Place";
                worksheet.Cell(1, 5).Value = "Date";
                worksheet.Cell(1, 6).Value = "Price";

                int row = 2;
                foreach (var item in products)
                {
                    worksheet.Cell(row, 1).Value = item.Id;
                    worksheet.Cell(row, 2).Value = item.Title;
                    worksheet.Cell(row, 3).Value = item.Description;
                    worksheet.Cell(row, 4).Value = item.Place;
                    worksheet.Cell(row, 5).Value = item.Date;
                    worksheet.Cell(row, 6).Value = item.Price;
                    row++;
                }

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Position = 0;

                    string fileName = $"ProductsReport_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName);
                }
            }
        }

        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportFromExcel(IFormFile excelFile)
        {
            if (excelFile == null || excelFile.Length == 0)
            {
                TempData["ImportMessage"] = "File isn't selectted or it's empty.";
                return RedirectToAction("Import");
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    await excelFile.CopyToAsync(stream);
                    stream.Position = 0;

                    using (var workbook = new ClosedXML.Excel.XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheet("Confereces");
                        int row = 2;
                        while (!worksheet.Cell(row, 1).IsEmpty())
                        {
                            var idCell = worksheet.Cell(row, 1).GetValue<string>();
                            var titleCell = worksheet.Cell(row, 2).GetValue<string>();
                            var descriptionCell = worksheet.Cell(row, 3).GetValue<string>();
                            var placeCell = worksheet.Cell(row, 4).GetValue<string>();
                            var dateCell = worksheet.Cell(row, 5).GetValue<string>();
                            var priceCell = worksheet.Cell(row, 6).GetValue<string>();

                            int id = 0;
                            int.TryParse(idCell, out id);

                            var conference = new Conference
                            {
                                Id = id,
                                Title = titleCell?.Trim(),
                                Description = descriptionCell?.Trim(),
                                Place = placeCell?.Trim(),
                                Date = DateTime.TryParse(dateCell, out DateTime date) ? date : DateTime.Now,
                                Price = int.TryParse(priceCell, out int price) ? price : 0,
                            };
                            _context.Conferences.Add(conference);

                            row++;
                        }
                    }
                }

                await _context.SaveChangesAsync();
                TempData["ImportMessage"] = "Import successful";
            }
            catch (Exception ex)
            {
                TempData["ImportMessage"] = $"Import error: {ex.Message}";
            }

            return RedirectToAction("Index");
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
        public async Task<IActionResult> Create([Bind("Title,Description,Place,Date,Price,PublicationId,OrganizatorId,Id")] Conference conference, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile?.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await imageFile.CopyToAsync(ms);

                    conference.ImageData = ms.ToArray();
                    conference.ImageMimeType = imageFile.ContentType; 
                }

                _context.Add(conference);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganizatorId"] = new SelectList(_context.Organizators, "Id", "Description", conference.OrganizatorId);
            ViewData["PublicationId"] = new SelectList(_context.Publications, "Id", "Title", conference.PublicationId);
            return View(conference);
        }

        public async Task<IActionResult> GetImage(int id)
        {
            var conf = await _context.Conferences
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.Id == id);

            if (conf == null || conf.ImageData == null)
                return NotFound();

            return File(conf.ImageData, conf.ImageMimeType!);
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
