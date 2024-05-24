using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class SettingService : ISettingService
    {
        ISettingRepository _settingRepository;

        public SettingService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public List<Setting> GetAllSettings(Func<Setting, bool>? filter = null)
        {
            return _settingRepository.GetAll(filter);
        }

        public Setting GetSetting(Func<Setting, bool>? filter = null)
        {
            return _settingRepository.Get(filter);
        }

        public void UpdateSetting(string key, string value)
        {
            Setting setting = _settingRepository.Get(x => x.Key == key);
            if(setting == null)
            {
                throw new NotFountSettingException("", $"{key} - Setting is null!");
            }
            setting.Value = value;
            _settingRepository.Commit();
        }
    }
}
