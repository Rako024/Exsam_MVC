using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exsam.Controllers
{
    public class HomeController : Controller
    {
        IOurServiceService _ourServiceService;

        public HomeController(IOurServiceService ourServiceService)
        {
            _ourServiceService = ourServiceService;
        }

        public IActionResult Index()
        {
            List<OurService> ourServices = _ourServiceService.GetAllOurServices();
            return View(ourServices);
        }
    }
}
