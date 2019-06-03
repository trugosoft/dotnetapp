using System;
using MongoDB.Bson;
using MongoDB.Driver;
using Pellucid.Core.Api.Model;
using System.Threading.Tasks;
using Pellucid.Core.Api.Interfaces;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

using MongoDB.Driver.Linq;

namespace Pellucid.Core.Api.Data
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AddressContext _context = null;

        public AddressRepository(IOptions<Settings> settings)
        {
            _context = new AddressContext(settings);
        }

        public async Task<IEnumerable<Address_Information>> GetAllAddress()
        {
            try
            {
                return await _context.Address.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // query after Id or InternalId (BSonId value)
        //
        public async Task<Address_Information> GetAddress(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.Address
                                .Find(address => address.pincode == id || address.pincode == internalId.ToString() ||  address.InternalId == internalId)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
        private ObjectId GetInternalId(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }
        // query after body text, updated time, and header image size
        //
        public async Task<IEnumerable<Address_Information>> GetAddress(string district, string state_name)
        {
            try
            {
                var query = _context.Address.Find(address => address.district.Contains(district) &&
                                       address.state_name.Contains(state_name));

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

    }
}
