using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pellucid.Core.Api.Model;
namespace Pellucid.Core.Api.Interfaces
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatient();

        Task<Patient> GetPatient(string id);

        // query after multiple parameters
        Task<IEnumerable<Patient>> GetPatient(string name, string mobile);

        // add new note document
        Task AddPatient(Patient item);

        // remove a single document / note
        //Task<bool> RemoveNote(string id);

        // update just a single document / note
        Task<bool> UpdatePatient(string id, string body);

        // demo interface - full document update
        Task<bool> UpdatePatientInformation(string id, string body);

        // should be used with high cautious, only in relation with demo setup
        Task<bool> RemoveAllPatients();

        // creates a sample index
        Task<string> CreateIndex();
    }
}
