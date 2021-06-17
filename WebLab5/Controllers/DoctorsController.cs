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
    public class DoctorsController : Controller
    {
        private readonly Context cntx;

        public DoctorsController(Context context)
        {
            cntx = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await cntx.Doctors.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Speciality")] Doctors doctorModel)
        {
            if (ModelState.IsValid)
            {
                cntx.Add(doctorModel);
                await cntx.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctorModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorModel = await cntx.Doctors.FindAsync(id);
            if (doctorModel == null)
            {
                return NotFound();
            }
            return View(doctorModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Speciality")] Doctors doctorModel)
        {
            if (id != doctorModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    cntx.Update(doctorModel);
                    await cntx.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorModelExists(doctorModel.Id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(doctorModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null) return NotFound();
            var doctorModel = await cntx.Doctors.FirstOrDefaultAsync(m => m.Id == id);
            if (doctorModel == null) return NotFound();

            return View(doctorModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return NotFound();

            var doctorModel = await cntx.Doctors.FirstOrDefaultAsync(m => m.Id == id);
            if (doctorModel == null) return NotFound();

            return View(doctorModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctorModel = await cntx.Doctors.FindAsync(id);
            cntx.Doctors.Remove(doctorModel);
            await cntx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorModelExists(int id)
        {
            return cntx.Doctors.Any(e => e.Id == id);
        }
    }
}
