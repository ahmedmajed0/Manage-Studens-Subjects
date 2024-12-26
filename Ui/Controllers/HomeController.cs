//using BL.Interfaces;
using BL.Interfaces;
using DAL.Interfaces;
using Domains;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Ui.Models;

namespace Ui.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShippingType _TypeServices;

        public HomeController(ILogger<HomeController> logger, IShippingType TypeServices)
        {
            _logger = logger;
            _TypeServices = TypeServices;
        }

        public IActionResult Index()
        {
            var types  = _TypeServices.GetAll();
            return View(types);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
