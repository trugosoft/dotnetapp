using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Pellucid.Core.Api.Model;

namespace Pellucid.Core.Api.Data
{
    public class AddressContext
    {
        private readonly IMongoDatabase _database = null;

        public AddressContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Address_Information> Address
        {
            get
            {
                return _database.GetCollection<Address_Information>("pincode_information");
            }
        }
    }
}
