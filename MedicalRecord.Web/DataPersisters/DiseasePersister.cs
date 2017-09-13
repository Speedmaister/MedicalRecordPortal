using MedicalRecord.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalRecord.Web.DataPersisters
{
    public class DiseasePersister
    {
        public void RemoveDisease(string disease, int patientId)
        {
            using (MedicalRecordContext context = new MedicalRecordContext())
            {
                var patientsDisease = context.Diseases.FirstOrDefault(x => x.PatientId == patientId && x.Name == disease);
                if(patientsDisease != null)
                {
                    context.Diseases.Remove(patientsDisease);
                    context.SaveChanges();
                }
            }
        }
    }
}