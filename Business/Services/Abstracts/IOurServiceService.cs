using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface IOurServiceService
    {
        void AddOurService(OurService ourService);
        void DeleteOurService(int id);
        void UpdateOurService(int id, OurService ourService);
        OurService GetOurService(Func<OurService,bool>? func = null);
        List<OurService> GetAllOurServices(Func<OurService,bool>? func = null);
    }
}
