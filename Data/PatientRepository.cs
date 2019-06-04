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
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientContext _context = null;

        public PatientRepository(IOptions<Settings> settings)
        {
            _context = new PatientContext(settings);
        }

        public async Task<IEnumerable<Patient>> GetAllPatient()
        {
            try
            {
                return await _context.Patient_list.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // query after Id or InternalId (BSonId value)
        //
        public async Task<Patient> GetPatient(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                return await _context.Patient_list
                                .Find(patient => patient.id == id || patient.InternalId == internalId)
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
        public async Task<IEnumerable<Patient>> GetPatient(string name, string phone_no)
        {
            try
            {
                var query = _context.Patient_list.Find(patient => patient.name.Contains(name) &&
                                       patient.primary_phone_no.Contains(phone_no));

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task AddPatient(Patient item)
        {
            try
            {
                await _context.Patient_list.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdatePatient(string id, string name)
        {
            var filter = Builders<Patient>.Filter.Eq(s => s.id, id);
            var update = Builders<Patient>.Update
                            .Set(s => s.name, name)
                            .CurrentDate(s => s.UpdatedOn);

            try
            {
                UpdateResult actionResult = await _context.Patient_list.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdatePatient(string id, Patient item)
        {
            try
            {
                ReplaceOneResult actionResult = await _context.Patient_list
                                                .ReplaceOneAsync(n => n.Equals(id)
                                                                , item
                                                                , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // Demo function - full document update
        public async Task<bool> UpdatePatientInformation(string id, string name)
        {
            var item = await GetPatient(id) ?? new Patient();
            item.name = name;
            item.UpdatedOn = DateTime.Now;

            return await UpdatePatient(id, item);
        }

        public async Task<bool> RemoveAllPatients()
        {
            try
            {
                DeleteResult actionResult = await _context.Patient_list.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // it creates a sample compound index (first using UserId, and then Body)
        // 
        // MongoDb automatically detects if the index already exists - in this case it just returns the index details
        public async Task<string> CreateIndex()
        {
            try
            {
                IndexKeysDefinition<Patient> keys = Builders<Patient>
                                                    .IndexKeys
                                                    .Ascending(item => item.name)
                                                    .Ascending(item => item.primary_phone_no);

                return await _context.Patient_list
                                .Indexes.CreateOneAsync(new CreateIndexModel<Patient>(keys));
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

    }
}
