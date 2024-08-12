using DiaryAgronomist.Context;
using DiaryAgronomist.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DiaryAgronomist.Controllers
{
    public class FuelOperationController : Controller
    {
        private readonly DaContext _context;

        public FuelOperationController(DaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetReceptionFuels()
        {
           
            List<ReceptionFuel> receptionFuels = await _context.ReceptionFuels
                .Include(f => f.Fuel)
                .Include(e => e.Employee)
                .Include(fs => fs.FuelSupplier)
                .ToListAsync();
            return View(receptionFuels);
        }

        public async Task<IActionResult> GetConsumptionFuels ()
        {
            List<ConsumptionFuel> consumptionFuels = await _context.ConsumptionFuels
                .Include(e => e.Employee)
                .Include(f => f.Fuel)
                .ToListAsync ();
            return View(consumptionFuels);
        }

        public async Task<IActionResult> CreateOrEditReceptionFuel(int? id)
        {
            List<Fuel> fuels = await _context.Fuels.ToListAsync();
            ViewBag.Fuels = new SelectList(fuels, "FuelId", "FuelName");
            List<Employee> employees = await _context.Employees.ToListAsync();
            ViewBag.Employees = new SelectList(employees, "EmployeeId", "FullName");
            List<FuelSupplier> fuelSuppliers = await _context.FuelSuppliers.ToListAsync();
            ViewBag.FuelSuppliers = new SelectList(fuelSuppliers, "SupplierId", "NameOrganization");

            ReceptionFuel receptionFuel = new ReceptionFuel();
            receptionFuel.DateReception = DateTime.Now;
            if (id != null && id != 0)
            {
                receptionFuel = _context.ReceptionFuels.FirstOrDefault(rf => rf.ReceptionId == id);
            }
            return PartialView("_CreateOrEditReceptionFuel", receptionFuel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEditReceptionFuel(ReceptionFuel receptionFuel)
        {
            _context.ReceptionFuels.Update(receptionFuel);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetReceptionFuels");
        }

        public IActionResult GetReceptionFuelForDelete(int? id)
        {
            ReceptionFuel reception = _context.ReceptionFuels.AsNoTracking()
                .Include(e => e.Employee)
                .Include(f => f.Fuel)
                .Include(fs => fs.FuelSupplier)
                .FirstOrDefault(r => r.ReceptionId == id);
            if (reception == null)
            { return NotFound(); }
            return PartialView("_DeleteReceptionFuel", reception);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteReceptionFuel(int? id)
        {
            if (id != null)
            {
                ReceptionFuel reception = await _context.ReceptionFuels.FirstOrDefaultAsync(r => r.ReceptionId == id);
                if (reception == null)
                {
                    return NotFound();
                }
                _context.Remove(reception);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetReceptionFuels");
            }
            return NotFound();
        }

        public async Task<IActionResult> CreateOrEditConsumptionFuel(int? id)
        {
            List<Fuel> fuels = await _context.Fuels.ToListAsync();
            ViewBag.Fuels = new SelectList(fuels, "FuelId", "FuelName");
            List<Employee> employees = await _context.Employees.ToListAsync();
            ViewBag.Employees = new SelectList(employees, "EmployeeId", "FullName");

            ConsumptionFuel consumptionFuel = new ConsumptionFuel();
            consumptionFuel.DateConsumption = DateTime.Now;
            if (id != null && id != 0)
            {
                consumptionFuel = _context.ConsumptionFuels.FirstOrDefault(cf => cf.ConsumptionId == id);
            }
            return PartialView("_CreateOrEditConsumptionFuel", consumptionFuel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEditConsumptionFuel(ConsumptionFuel consumptionFuel)
        {
            _context.ConsumptionFuels.Update(consumptionFuel);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetConsumptionFuels");
        }

        public IActionResult GetConsumptionFuelForDelete(int? id)
        {
            ConsumptionFuel consumption = _context.ConsumptionFuels.AsNoTracking()
                .Include(e =>e.Employee)
                .Include(f => f.Fuel)
                .FirstOrDefault(e => e.ConsumptionId == id);
            if (consumption == null)
            { return NotFound(); }
            return PartialView("_DeleteConsumptionFuel", consumption);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConsumptionFuel(int? id)
        {
            if (id != null)
            {
                ConsumptionFuel consumption = await _context.ConsumptionFuels.FirstOrDefaultAsync(e => e.ConsumptionId == id);
                if (consumption == null)
                {
                    return NotFound();
                }
                _context.Remove(consumption);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetConsumptionFuels");
            }
            return NotFound();
        }

    }
    
}
