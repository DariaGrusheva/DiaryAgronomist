using DiaryAgronomist.Context;
using DiaryAgronomist.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiaryAgronomist.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DaContext _context;

        public EmployeeController(DaContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> GetEmployees()
        {
            List<Employee> employees = await _context.Employees.OrderBy(e => e.EmployeeId)
                .ToListAsync();
            return View(employees);
        }
         
        public IActionResult CreateOrEditEmployee(int? id) 
        {
            Employee employee = new Employee();
            employee.DismissalDate = DateTime.Now;
            employee.EmploymentDate = DateTime.Now;
            if (id != null || id !=0)
            { 
                employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
            }
            return PartialView("_CreateOrEditEmployee", employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEditEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetEmployees");
        }

        public IActionResult GetEmployeeForDelete(int? id)
        {
            Employee employee = _context.Employees.AsNoTracking().FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            { return NotFound(); }
            return PartialView("_DeleteEmployee", employee);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int? id)
        {
            if (id != null)
            {
                Employee employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
                if (employee == null)
                {
                    return NotFound();
                }
                _context.Remove(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetEmployees");
            }
            return NotFound();
        }
    }
}
