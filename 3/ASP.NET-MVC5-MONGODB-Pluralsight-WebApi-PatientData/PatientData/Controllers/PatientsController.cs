using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using MongoDB.Driver;
using PatientData.Models;

namespace PatientData.Controllers
{
    [Authorize]
    //[EnableCors("*", "*", "GET")]
    public class PatientsController : ApiController
    {
        // -----------------------------------------------------------------------------------------
        
        private IMongoCollection<Patient> _patients;

        // -----------------------------------------------------------------------------------------

        public PatientsController()
        {
            _patients = PatientDb.Open();
        }

        // -----------------------------------------------------------------------------------------

        // api/patients
        public IEnumerable<Patient> Get()
        {
            return _patients.Find(_ => true).ToList();
        }

        // -----------------------------------------------------------------------------------------

        // api/patients/57e540aa2372ff1ab4c04bbe
        [Route("api/patients/{id}")]
        public IHttpActionResult Get(string id)
        {
            var patient = GetPatient(id);
            if (patient != null)
            {
                return Ok(patient);
            }
            return NotFound();

        }

        // -----------------------------------------------------------------------------------------

        [Route("api/patients/{id}/medications")]
        public IHttpActionResult GetMedications(string id)
        {
            var patient = GetPatient(id);
            if (patient == null)
            {
                return NotFound();

            }
            var medications = patient.Medications;
            return Ok(medications);
        }

        // -----------------------------------------------------------------------------------------

        private Patient GetPatient(string id)
        {
            var patients = Get();
            foreach (var patient in patients)
            {
                if (patient.Id == id)
                {
                    return patient;
                }
            }

            return null;
        }

    }
}
