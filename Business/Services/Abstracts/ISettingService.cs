using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface ISettingService
    {
        List<Setting> GetAllSettings(Func<Setting, bool>? filter = null);
        Setting GetSetting(Func<Setting, bool>? filter = null);
        void UpdateSetting(string key,string value);
    }
}
