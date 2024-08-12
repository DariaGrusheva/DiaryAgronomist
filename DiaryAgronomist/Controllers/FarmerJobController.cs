using DiaryAgronomist.Context;
using DiaryAgronomist.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DiaryAgronomist.Controllers
{
    public class FarmerJobController : Controller
    {
        private readonly DaContext _context;

        public FarmerJobController(DaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetSowing ()
        {
            List<Sowing> sowings = await _context.Sowings
                .Include(c => c.Cereal)
                .Include(f => f.FieldPlanting)
                .Include(se => se.SowingEmployees)
                .ThenInclude(e => e.Employee)
                .Include (sm => sm.SowingTechniques)
                .ThenInclude(m => m.Machinery)
                .ToListAsync();
            return View(sowings);
        }

        public async Task<IActionResult> GetHarvesting()
        {
            List<Harvesting> harvestings= await _context.Harvestings
                .Include(c => c.Cereal)
                .Include(f => f.FieldPlanting)
                .Include(he => he.Employees)
                .Include(hm => hm.Machineries)
                .ToListAsync();
            return View(harvestings);
        }

        public async Task<IActionResult> CreateOrEditSowing(int id)
        {
            List<Cereal> cereals = await _context.Cereals.ToListAsync();
            ViewBag.Cereals = new SelectList(cereals, "CerealId", "CerealName");
            List<FieldPlanting> fieldPlantings = await _context.FieldPlantings.ToListAsync();
            ViewBag.Fields = new SelectList(fieldPlantings, "FieldId", "FieldAddress");
            List<Employee> employees = await _context.Employees.ToListAsync();
            ViewBag.Employees = new SelectList(employees, "EmployeeId", "FullName");
            List<Machinery> machineries = await _context.Machineries.ToListAsync();
            ViewBag.Machineries = new SelectList(machineries, "MachineryId", "MachineryName");
            Sowing sowing = new Sowing();
            sowing.SowingDate = DateTime.Now.Date;
            if (id != 0)
            {
                sowing = await _context.Sowings
                    .Include(e => e.Employees)
                    .Include(m => m.Machineries)
                    .FirstOrDefaultAsync(s => s.SowingId == id);
                if (sowing.Employees.Count() > 0)
                {
                    ViewBag.SelectedEmployees = new SelectList(sowing.Employees.Select(s => s.EmployeeId));
                }
                if (sowing.Machineries.Count() > 0)
                {
                    ViewBag.SelectedMachineries = new SelectList(sowing.Machineries.Select(m => m.MachineryId));
                }
            }
            return View("CreateOrEditSowing", sowing);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEditSowing(Sowing sowing, int[] employeesId, int[] machineriesId)
        {
            Sowing sowSaveInBase = new Sowing();
            if (sowing.SowingId != 0)
            {
                sowSaveInBase = await _context.Sowings
                    .Where(h => h.SowingId == sowing.SowingId)
                    .Include(h => h.SowingEmployees)
                    .Include(m => m.SowingTechniques)
                    .FirstOrDefaultAsync();
                sowSaveInBase.SowingEmployees.Clear();
                sowSaveInBase.SowingTechniques.Clear();
            }
            sowSaveInBase.SowingDate = sowing.SowingDate;
            sowSaveInBase.FieldId = sowing.FieldId;
            sowSaveInBase.CerealId = sowing.CerealId;
            sowSaveInBase.NumberCereal = sowing.NumberCereal;
            sowSaveInBase.SownArea = sowing.SownArea;

            foreach (int id in employeesId)
            {
                SowingEmployee se = new SowingEmployee { SowingId = sowing.SowingId, EmployeeId = id };
                sowSaveInBase.SowingEmployees
                    .Add(se);
            }
            foreach (int id in machineriesId)
            {
                sowSaveInBase.SowingTechniques
                    .Add(new SowingTechnique { SowingId = sowing.SowingId, MachineryId = id });
            }

            if (sowing.SowingId == 0)
                _context.Sowings.Add(sowSaveInBase);
            else
                _context.Entry(sowSaveInBase).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("GetSowing");
        }

        public IActionResult GetSowingForDelete(int? id)
        {
            Sowing sowing = _context.Sowings.AsNoTracking()
                .Include(c => c.Cereal)
                .Include(f => f.FieldPlanting)
                .Include(se => se.Employees)
                .Include(sm => sm.Machineries)
                .FirstOrDefault(r => r.SowingId == id);
            if (sowing == null)
            { return NotFound(); }
            return PartialView("_DeleteSowing", sowing);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSowing(int? id)
        {
            if (id != null)
            {
                Sowing sowing = await _context.Sowings
                    .Include(f => f.FieldPlanting)
                    .Include(c => c.Cereal)
                    .FirstOrDefaultAsync(r => r.SowingId == id);
                if (sowing == null)
                {
                    return NotFound();
                }
                _context.Remove(sowing);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetSowing");
            }
            return NotFound();
        }

        public async Task<IActionResult> CreateOrEditHarvesting(int id)
        {
            List<Cereal> cereals = await _context.Cereals.ToListAsync();
            ViewBag.Cereals = new SelectList(cereals, "CerealId", "CerealName");
            List<FieldPlanting> fieldPlantings = await _context.FieldPlantings.ToListAsync();
            ViewBag.Fields = new SelectList(fieldPlantings, "FieldId", "FieldAddress");
            List<Employee> employees = await _context.Employees.ToListAsync();
            ViewBag.Employees = new SelectList(employees, "EmployeeId", "FullName");
            List<Machinery> machineries = await _context.Machineries.ToListAsync();
            ViewBag.Machineries = new SelectList(machineries, "MachineryId", "MachineryName");
            Harvesting harvesting = new Harvesting();
            harvesting.HarvestingDate = DateTime.Now.Date;

            if (id != 0)
            {
                harvesting = await _context.Harvestings
                    .Include(e => e.Employees)
                    .Include(m => m.Machineries)
                    .FirstOrDefaultAsync(s => s.HarvestingId == id);
                if (harvesting.Employees.Count() > 0) {
                    ViewBag.SelectedEmployees = new SelectList(harvesting.Employees.Select(s => s.EmployeeId));
                }
                if (harvesting.Machineries.Count() > 0)
                {
                    ViewBag.SelectedMachineries = new SelectList(harvesting.Machineries.Select(m => m.MachineryId));
                }
            }
            return View("CreateOrEditHarvesting", harvesting);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEditHarvesting(Harvesting harvesting, int[] employeesId, int[] machineriesId)
        {
            Harvesting harvSaveInBase = new Harvesting();
            if (harvesting.HarvestingId != 0)
            {
                harvSaveInBase = await _context.Harvestings
                    .Where(h => h.HarvestingId == harvesting.HarvestingId)
                    .Include(h => h.HarvestingEmployees)
                    .Include(m => m.HarvestingTechniques)
                    .FirstOrDefaultAsync();
                harvSaveInBase.HarvestingEmployees.Clear();
                harvSaveInBase.HarvestingTechniques.Clear();
            }
            harvSaveInBase.HarvestingDate = harvesting.HarvestingDate;
            harvSaveInBase.FieldId = harvesting.FieldId;
            harvSaveInBase.CerealId = harvesting.CerealId;
            harvSaveInBase.NumberCereal = harvesting.NumberCereal;

            foreach (int id in employeesId)
            {
                HarvestingEmployee he = new HarvestingEmployee { HarvestingId = harvesting.HarvestingId, EmployeeId = id };
                harvSaveInBase.HarvestingEmployees
                    .Add(he);
            }
            foreach (int id in machineriesId)
            {
                harvSaveInBase.HarvestingTechniques
                    .Add(new HarvestingTechnique { HarvestingId = harvesting.HarvestingId, MachineryId = id });
            }

            if (harvesting.HarvestingId == 0)
                _context.Harvestings.Add(harvSaveInBase);
            else
                _context.Entry(harvSaveInBase).State = EntityState.Modified;
            //_context.Harvestings.Update(harvSaveInBase);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetHarvesting");
        }

        public IActionResult GetHarvestingForDelete(int? id)
        {
            Harvesting harvesting = _context.Harvestings.AsNoTracking()
                .Include(c => c.Cereal)
                .Include(f => f.FieldPlanting)
                .Include(he => he.Employees)
                .Include(hm => hm.Machineries)
                .FirstOrDefault(r => r.HarvestingId == id);
            if (harvesting == null)
            { return NotFound(); }
            return PartialView("_DeleteHarvesting", harvesting);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteHarvesting(int? id)
        {
            if (id != null)
            {
                Harvesting harvesting = await _context.Harvestings.FirstOrDefaultAsync(r => r.HarvestingId == id);

                if (harvesting == null)
                {
                    return NotFound();
                }
                _context.Remove(harvesting);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetHarvesting");
            }
            return NotFound();
        }
    }
}
