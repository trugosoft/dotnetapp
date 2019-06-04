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
                    int _age = (new Random().Next(1, 100));

                    string someJson = @"{""Sameinfo"": """",""nested"": {""nested 2"": 111,""nested"": {""nested 3"": 0,""Verbose"": 20},""ErrorDate"": """"}}";
                    _patientRepository.AddPatient(new Patient()
                    {
                        id = "Pelucid_Client_" + i.ToString(),
                        name = "Pellucid_" + i.ToString(),
                        age = _age.ToString(),
                        sex = i % 2 == 0 ? "M" : "F",
                        DOB = DateTime.Now.AddYears(-_age),
                        UpdatedOn = DateTime.Now,
                        title = "MR",
                        some_data = (new Random().Next(98929202, 99999999)).ToString(),
                        primary_phone_no = (new Random().Next(98929202, 99999999)).ToString(),
                        Addtional_information = MongoDB.Bson.BsonDocument.Parse(someJson)
                        // Addtional_information = Newtonsoft.Json.Linq.JObject.Parse(someJson)

                    }); ;

                }
                return "Database with 100 patient information created ";
            }

            return "Unknown";
        }
    }
}
