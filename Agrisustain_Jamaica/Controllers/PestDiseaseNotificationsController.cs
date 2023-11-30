using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agrisustain_Jamaica.Data;
using Agrisustain_Jamaica.Models;

namespace Agrisustain_Jamaica.Controllers
{
    public class PestDiseaseNotificationsController : Controller
    {
        private readonly Agrisustain_JamaicaContext _context;

        public PestDiseaseNotificationsController(Agrisustain_JamaicaContext context)
        {
            _context = context;
        }

        // GET: PestDiseaseNotifications
        public async Task<IActionResult> Index()
        {
              return _context.PestDiseaseSubmission != null ? 
                          View(await _context.PestDiseaseSubmission.ToListAsync()) :
                          Problem("Entity set 'Agrisustain_JamaicaContext.PestDiseaseSubmission'  is null.");
        }

        // GET: PestDiseaseNotifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PestDiseaseSubmission == null)
            {
                return NotFound();
            }

            var pestDiseaseSubmission = await _context.PestDiseaseSubmission
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pestDiseaseSubmission == null)
            {
                return NotFound();
            }

            return View(pestDiseaseSubmission);
        }

        // GET: PestDiseaseNotifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PestDiseaseNotifications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FarmLocation,DateOfSighting,Pest,Disease,Description,PhotoPath")] PestDiseaseSubmission pestDiseaseSubmission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pestDiseaseSubmission);
                await _context.SaveChangesAsync();
                TempData["Notification"] = "Pest or disease found on a nearby farm";
                return RedirectToAction(nameof(Index));
            }
            return View(pestDiseaseSubmission);
        }

        // GET: PestDiseaseNotifications/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PestDiseaseSubmission == null)
            {
                return NotFound();
            }

            var pestDiseaseSubmission = await _context.PestDiseaseSubmission.FindAsync(id);
            if (pestDiseaseSubmission == null)
            {
                return NotFound();
            }
            return View(pestDiseaseSubmission);
        }

        // POST: PestDiseaseNotifications
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FarmLocation,DateOfSighting,Pest,Disease,Description,PhotoPath")] PestDiseaseSubmission pestDiseaseSubmission)
        {
            if (id != pestDiseaseSubmission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pestDiseaseSubmission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PestDiseaseSubmissionExists(pestDiseaseSubmission.Id))
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
            return View(pestDiseaseSubmission);
        }

        // GET: PestDiseaseNotifications/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PestDiseaseSubmission == null)
            {
                return NotFound();
            }

            var pestDiseaseSubmission = await _context.PestDiseaseSubmission
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pestDiseaseSubmission == null)
            {
                return NotFound();
            }

            return View(pestDiseaseSubmission);
        }

        // POST: PestDiseaseNotifications/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PestDiseaseSubmission == null)
            {
                return Problem("Entity set 'Agrisustain_JamaicaContext.PestDiseaseSubmission'  is null.");
            }
            var pestDiseaseSubmission = await _context.PestDiseaseSubmission.FindAsync(id);
            if (pestDiseaseSubmission != null)
            {
                _context.PestDiseaseSubmission.Remove(pestDiseaseSubmission);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PestDiseaseSubmissionExists(int id)
        {
          return (_context.PestDiseaseSubmission?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
