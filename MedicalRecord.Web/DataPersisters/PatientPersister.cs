using MedicalRecord.Data;
using MedicalRecord.Web.Models;
using System.Data.Entity;
using System.Linq;
using System;

namespace MedicalRecord.Web.DataPersisters
{
    public class PatientPersister
    {
        public PatientModel Get(int id)
        {
            PatientModel patientModel = null;
            using (MedicalRecordContext context = new MedicalRecordContext())
            {
                var patient = context.Patients.Include(x => x.TeethStatus).Include(x => x.Diseases).FirstOrDefault(x => x.Id == id);
                if (patient != null)
                {
                    patientModel = new PatientModel(patient);
                }

                return patientModel;
            }
        }

        public int Save(PatientModel patient)
        {
            
        }
    }
}