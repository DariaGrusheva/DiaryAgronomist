using DiaryAgronomist.Context;
using DiaryAgronomist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DiaryAgronomist.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DaContext _context;

        public HomeController(ILogger<HomeController> logger, DaContext context)
        {
            _logger = logger;
            _context = context;
        }
       

        public async Task<IActionResult> Index()
        {
            List<HarvestingEmployee> employees = await _context.HarvestingEmployees
                .ToListAsync();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
       
    }
}
