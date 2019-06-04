using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pellucid.Core.Api.Interfaces;
using Pellucid.Core.Api.Model;
using Pellucid.Core.Api.Infrastructure;
using System;
using System.Collections.Generic;
namespace Pellucid.Core.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PatientsController : Controller
    {
        private readonly IPatientRepository _patientRepository;

        public PatientsController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [NoCache]
        [HttpGet]
        public async Task<IEnumerable<Patient>> Get()
        {
            return await _patientRepository.GetAllPatient();
        }

        // GET api/notes/5
        [HttpGet("{id}")]
        public async Task<Patient> Get(string id)
        {
            return await _patientRepository.GetPatient(id) ?? new Patient();
        }

        // GET api/notes/text/date/size
        // ex: http://localhost:53617/api/notes/Test/2018-01-01/10000
        [NoCache]
        [HttpGet(template: "{bodyText}/{updatedFrom}/{headerSizeLimit}")]
        public async Task<IEnumerable<Patient>> Get(string name, string phone_no)
        {
            return await _patientRepository.GetPatient(name, phone_no)
                        ?? new List<Patient>();
        }
        // POST api/notes
        [HttpPost]
        public void Post([FromBody] Patient_Params param)
        {
            _patientRepository.AddPatient(new Patient
            {
                id = param.Id,
                name = param.name,
                UpdatedOn = DateTime.Now
            });
        }

        // PUT api/notes/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string value)
        {
            _patientRepository.UpdatePatientInformation(id, value);
        }

    }
}