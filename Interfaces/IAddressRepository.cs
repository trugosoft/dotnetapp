using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pellucid.Core.Api.Model;
namespace Pellucid.Core.Api.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address_Information>> GetAllAddress();

        Task<Address_Information> GetAddress(string id);

        // query after multiple parameters
        Task<IEnumerable<Address_Information>> GetAddress(string district, string state_name);
    }
}
