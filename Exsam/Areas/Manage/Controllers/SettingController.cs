using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exsam.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="Admin")]
    public class SettingController : Controller
    {
        ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public IActionResult Index()
        {
            List<Setting> settingList = _settingService.GetAllSettings();
            return View(settingList);
        }

        public IActionResult Update(string key)
        {
            Setting setting = _settingService.GetSetting(x => x.Key == key);
            if(setting == null)
            {
                return View("Error");
            }
            return View(setting);
        }
        [HttpPost]
        public IActionResult Update(Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _settingService.UpdateSetting(setting.Key, setting.Value);
            }catch(NotFountSettingException ex)
            {
                return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }
    }
}
