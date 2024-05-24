using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Exsam.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class OurServiceController : Controller
    {
        IOurServiceService _service;

        public OurServiceController(IOurServiceService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            List<OurService> servives = _service.GetAllOurServices();
            return View(servives);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(OurService our)
        {
            if (!ModelState.IsValid)
            {
                return View();   
            }
            try
            {
                _service.AddOurService(our);
            }
            catch(NotFoundOurServiceException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            try
            {
                _service.DeleteOurService(id);
            }catch(NotFoundOurServiceException ex)
            {
                return View("Error");
            }
           
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            OurService our = _service.GetOurService(x => x.Id == id);
            if(our == null)
            {
                return View("Error");
            }
            return View(our);
        }

        [HttpPost]
        public IActionResult Update(OurService our)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _service.UpdateOurService(our.Id, our);
            }catch(NotFoundOurServiceException ex)
            {
                return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
