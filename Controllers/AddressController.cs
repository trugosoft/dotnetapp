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
    public class AddressController : Controller
    {
        private readonly IAddressRepository _addressRepository;

        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [NoCache]
        [HttpGet]
        public async Task<IEnumerable<Address_Information>> Get()
        {
            return await _addressRepository.GetAllAddress();
        }

        // GET api/notes/5
        [HttpGet("{id}")]
        public async Task<Address_Information> Get(string id)
        {
            return await _addressRepository.GetAddress(id) ?? new Address_Information();
        }

        // GET api/notes/text/date/size
        // ex: http://localhost:53617/api/notes/Test/2018-01-01/10000
        [NoCache]
        [HttpGet(template: "{bodyText}/{updatedFrom}/{headerSizeLimit}")]
        public async Task<IEnumerable<Address_Information>> Get(string district, string state)
        {
            return await _addressRepository.GetAddress(district, state)
                        ?? new List<Address_Information>();
        }

    }
}