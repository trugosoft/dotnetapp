using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Pellucid.Core.Api.Model;

namespace Pellucid.Core.Api.Data
{
    public class PatientContext
    {
        private readonly IMongoDatabase _database = null;

        public PatientContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Patient> Patient_list
        {
            get
            {
                return _database.GetCollection<Patient>("patient");
            }
        }
    }
}
