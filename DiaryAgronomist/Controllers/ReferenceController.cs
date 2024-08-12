using DiaryAgronomist.Context;
using DiaryAgronomist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.Xml;

namespace DiaryAgronomist.Controllers
{
    public class ReferenceController : Controller
    {
        private readonly DaContext _context;

        public ReferenceController(DaContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetFields()
        {
            List<FieldPlanting> fieldPlantings = await _context.FieldPlantings.OrderBy(f => f.FieldId)
                .ToListAsync();
            return View(fieldPlantings);
        }

        public async Task<IActionResult> GetCereals()
        {
            List<Cereal> cereals = await _context.Cereals.OrderBy(f => f.CerealId)
                .ToListAsync();
            return View(cereals);
        }

        public async Task<IActionResult> GetMachineries()
        {
            List<Machinery> machineries = await _context.Machineries.OrderBy(f => f.MachineryId)
                .ToListAsync();
            return View(machineries);
        }

        public async Task<IActionResult> GetFuels()
        {
            List<Fuel> fuels = await _context.Fuels.OrderBy(f => f.FuelId)
                .ToListAsync();
            return View(fuels);
        }

        public async Task<IActionResult> GetFuelSuppliers()
        {
            List<FuelSupplier> fuelSuppliers = await _context.FuelSuppliers.OrderBy(fs => fs.SupplierId)
                .ToListAsync();
            return View(fuelSuppliers);
        }


        //добавление и изменение данных
        public IActionResult CreateOrEditField(int? id)
        {
            FieldPlanting fieldPlanting = new FieldPlanting();
            if (id == null)
            {
                return PartialView("_CreateOrEditField");
            }
            else
            {
                fieldPlanting = _context.FieldPlantings.FirstOrDefault(f => f.FieldId == id);
            }
            return PartialView("_CreateOrEditField", fieldPlanting);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEditField(FieldPlanting fieldPlanting)

        {
            _context.FieldPlantings.Update(fieldPlanting);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetFields");
        }

        public IActionResult GetFieldForDelete(int? id)
        {
            FieldPlanting fieldPlanting = _context.FieldPlantings.AsNoTracking().FirstOrDefault(fp => fp.FieldId == id);
            if (fieldPlanting == null)
            { return NotFound(); }
            return PartialView("_DeleteField", fieldPlanting);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteField(int? id)
        {
            if (id != null)
            {
                FieldPlanting fieldPlanting = await _context.FieldPlantings.FirstOrDefaultAsync(fp => fp.FieldId == id);
                if (fieldPlanting == null)
                {
                    return NotFound();
                }
                _context.Remove(fieldPlanting);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetFields");
            }
            return NotFound();
        }

        public IActionResult CreateOrEditCereal(int? id)
        {
            Cereal cereal = new Cereal();
            if (id == null)
            {
                return PartialView("_CreateOrEditCereal");
            }
            else
            {
                cereal = _context.Cereals.FirstOrDefault(c => c.CerealId == id);
            }
            return PartialView("_CreateOrEditCereal", cereal);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEditCereal(int? id, Cereal cereal)
        {
            _context.Cereals.Update(cereal);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetCereals");
        }

        public IActionResult GetCerealForDelete(int? id)
        {
            Cereal cereal = _context.Cereals.AsNoTracking().FirstOrDefault(ce => ce.CerealId == id);
            if (cereal == null)
            { return NotFound(); }
            return PartialView("_DeleteCereal", cereal);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCereal(int? id)
        {
            if (id != null)
            {
                Cereal cerealf = await _context.Cereals.FirstOrDefaultAsync(c => c.CerealId == id);
                if (cerealf == null)
                {
                    return NotFound();
                }
                _context.Remove(cerealf);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetCereals");
            }
            return NotFound();
        }
    
              

        public IActionResult CreateOrEditMachinery(int? id)
        {
            Machinery machinery = new Machinery();
            if (id == null)
            {
                return PartialView("_CreateOrEditMachinery");
            }
            else
            {
                machinery = _context.Machineries.FirstOrDefault(m => m.MachineryId == id);
            }
            return PartialView("_CreateOrEditMachinery", machinery);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEditMachinery(int? id, Machinery machinery)
        {
            _context.Machineries.Update(machinery);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetMachineries");
        }

        public IActionResult GetMachineryForDelete(int? id)
        {
            Machinery machinery = _context.Machineries.AsNoTracking().FirstOrDefault(m => m.MachineryId == id);
            if (machinery == null)
            { return NotFound(); }
            return PartialView("_DeleteMachinery", machinery);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteMachinery(int? id)
        {
            if (id != null)
            {
                Machinery machinery = await _context.Machineries.FirstOrDefaultAsync(m => m.MachineryId == id);
                if (machinery == null)
                {
                    return NotFound();
                }
                _context.Remove(machinery);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetMachineries");
            }
            return NotFound();
        }

        public IActionResult CreateOrEditFuel(int? id) 
        {
            Fuel fuel = new Fuel();
            if (id != null && id != 0)
            {
                
                fuel = _context.Fuels.FirstOrDefault(f => f.FuelId == id);
            }
            return PartialView("_CreateOrEditFuel", fuel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEditFuel(Fuel fuel)
        {
            _context.Fuels.Update(fuel);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetFuels");           
        }

        public IActionResult GetFuelForDelete(int? id)
        {
            Fuel fuel = _context.Fuels.AsNoTracking().FirstOrDefault(f => f.FuelId == id);
            if (fuel == null)
            { return NotFound(); }
            return PartialView("_DeleteFuel", fuel);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteFuel(int? id)
        {
            if (id != null)
            {
                Fuel fuel = await _context.Fuels.FirstOrDefaultAsync(f => f.FuelId == id);
                if (fuel == null)
                {
                    return NotFound();
                }
                _context.Remove(fuel);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetFuels");
            }
            return NotFound();
        }

        public IActionResult CreateOrEditFuelSupplier(int? id)
        {
            FuelSupplier fuelSupplier = new FuelSupplier();
            if (id != null && id != 0)
            {
                fuelSupplier = _context.FuelSuppliers.FirstOrDefault(fs => fs.SupplierId == id);
            }
            return PartialView("_CreateOrEditFuelSupplier", fuelSupplier);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEditFuelSupplier(FuelSupplier fuelSupplier)
        {
            _context.FuelSuppliers.Update(fuelSupplier);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetFuelSuppliers");
        }

        public IActionResult GetFuelSupplierForDelete(int? id)
        {
            FuelSupplier fuelsup = _context.FuelSuppliers.AsNoTracking().FirstOrDefault(fs => fs.SupplierId == id);
            if (fuelsup == null)
            { return NotFound(); }
            return PartialView("_DeleteFuelSupplier", fuelsup);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteFuelSupplier(int? id)
        {
            if (id != null)
            {
                FuelSupplier fuelsup = await _context.FuelSuppliers.FirstOrDefaultAsync(fs => fs.SupplierId == id);
                if (fuelsup == null)
                {
                    return NotFound();
                }
                _context.Remove(fuelsup);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetFuelSuppliers");
            }
            return NotFound();
        }

    }
}
