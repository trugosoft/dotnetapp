using System;
using Microsoft.AspNetCore.Mvc;

using Pellucid.Core.Api.Interfaces;
using Pellucid.Core.Api.Model;

namespace Pellucid.Core.Api.Controllers
{
    [Route("api/[controller]")]
    public class SystemController : Controller
    {
        private readonly IPatientRepository _patientRepository;

        public SystemController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        // Call an initialization - api/system/init
        [HttpGet("{setting}")]
        public string Get(string setting)
        {
            if (setting == "init")
            {

                var name = _patientRepository.CreateIndex();
                _patientRepository.RemoveAllPatients();
                for(int i=0; i<100;i++)
                {
                    _patientRepository.AddPatient(new Patient()
                    {
                        id = "00" + i.ToString(),
                        name = "Pellucid_" + i.ToString(),
                        age = (new Random().Next(0, 100)).ToString(),
                        sex = i % 2 ==0 ? "M" : "F",
                        UpdatedOn = DateTime.Now,
                        primary_phone_no = (new Random().Next(98929202, 99999999)).ToString()

                    }) ;

                }
                return "Database with 100 patient information created ";
            }

            return "Unknown";
        }
    }
}
