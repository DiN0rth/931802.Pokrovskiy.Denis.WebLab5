using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebLab5.Models;

namespace WebLab5.Controllers
{
    public class PatientsController : Controller
    {
        private readonly Context cntx;

        public PatientsController(Context context)
        {
            cntx = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await cntx.Patients.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Diagnosis, Phones, Age")] Patients patientModel)
        {
            if (ModelState.IsValid)
            {
                cntx.Add(patientModel);
                await cntx.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patientModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null) return NotFound();

            var patientModel = await cntx.Patients.FindAsync(id);
            if (patientModel == null) return NotFound();

            return View(patientModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Diagnosis,Phones, Age")] Patients patientModel)
        {
            if (id != patientModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    cntx.Update(patientModel);
                    await cntx.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientModelExists(patientModel.Id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patientModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null) return NotFound();

            var patientModel = await cntx.Patients.FirstOrDefaultAsync(m => m.Id == id);
            if (patientModel == null) return NotFound();

            return View(patientModel);
        }



        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return NotFound();

            var patientModel = await cntx.Patients.FirstOrDefaultAsync(m => m.Id == id);
            if (patientModel == null) return NotFound();

            return View(patientModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patientModel = await cntx.Patients.FindAsync(id);
            cntx.Patients.Remove(patientModel);
            await cntx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientModelExists(int id)
        {
            return cntx.Patients.Any(e => e.Id == id);
        }
    }
}
