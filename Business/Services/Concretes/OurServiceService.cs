using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class OurServiceService : IOurServiceService
    {
        IOurServiceRepository _ourServiceRepository;
        IWebHostEnvironment _webHostEnvironment;
        public OurServiceService(IOurServiceRepository ourServiceRepository, IWebHostEnvironment webHostEnvironment)
        {
            _ourServiceRepository = ourServiceRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void AddOurService(OurService ourService)
        {
            if(ourService == null)
            {
                throw new NotFoundOurServiceException("", "Our Servie is null!");
            }
          
            _ourServiceRepository.Add(ourService);
            _ourServiceRepository.Commit();
        }

        public void DeleteOurService(int id)
        {
            OurService our = _ourServiceRepository.Get(x => x.Id == id);
            if(our == null)
            {
                throw new NotFoundOurServiceException("", "OurService is null!");
            }
            _ourServiceRepository.Delete(our);
            _ourServiceRepository.Commit();
        }

        public List<OurService> GetAllOurServices(Func<OurService, bool>? func = null)
        {
            return _ourServiceRepository.GetAll(func);
        }

        public OurService GetOurService(Func<OurService, bool>? func = null)
        {
            return _ourServiceRepository.Get(func);
        }

        public void UpdateOurService(int id, OurService ourService)
        {
            OurService our = _ourServiceRepository.Get(x => x.Id == id);
            if (our == null)
            {
                throw new NotFoundOurServiceException("", "OurService is null!");
            }
           
            our.Title = ourService.Title;
            our.SubTitle = ourService.SubTitle;
            our.Description = ourService.Description;
            our.IconTag = ourService.IconTag;
            _ourServiceRepository.Commit();
        }
    }
}
