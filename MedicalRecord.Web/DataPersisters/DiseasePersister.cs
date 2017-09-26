using MedicalRecord.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalRecord.Web.DataPersisters
{
    public class DiseasePersister
    {
        public void RemoveDisease(int diseaseId, int patientId)
        {
            using (MedicalRecordContext context = new MedicalRecordContext())
            {
                var patientsDisease = context.Diseases.FirstOrDefault(x => x.PatientId == patientId && x.Id == diseaseId);
                if (patientsDisease != null)
                {
                    context.Diseases.Remove(patientsDisease);
                    context.SaveChanges();
                }
            }
        }

        public Disease AddDisease(string diseaseName, int patientId)
        {
            using (MedicalRecordContext context = new MedicalRecordContext())
            {
                if (!context.Patients.Any(x => x.Id == patientId))
                {
                    throw new ArgumentException("Invalid patient.");
                }

                var disease = new Disease() { Name = diseaseName, PatientId = patientId };
                context.Diseases.Add(disease);
                context.SaveChanges();
                return disease;
            }
        }
    }
}