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

        public void Save(PatientModel patientModel)
        {
            using (MedicalRecordContext context = new MedicalRecordContext())
            {
                var patient = patientModel.ToPatient();
                var patientDbEntity = context.Patients
                                                .Include(x => x.Diseases)
                                                .Include(x => x.TeethStatus)
                                                .FirstOrDefault(x => x.Id == patient.Id);
                patientDbEntity.FirstName = patient.FirstName;
                patientDbEntity.LastName = patient.LastName;
                patientDbEntity.SurName = patient.SurName;
                patientDbEntity.Address = patient.Address;
                patientDbEntity.EGN = patient.EGN;
                patientDbEntity.Telephone = patient.Telephone;

                // Add new diseases
                for (int i = 0; i < patient.Diseases.Count; i++)
                {
                    var disease = patient.Diseases[i];
                    if (disease.Id == 0)
                    {
                        patientDbEntity.Diseases.Add(disease);
                    }
                }

                // Remove deleted diseases
                for (int i = 0; i < patientDbEntity.Diseases.Count; i++)
                {
                    var diseaseDb = patientDbEntity.Diseases[i];
                    if (!patient.Diseases.Any(x => x.Id == diseaseDb.Id))
                    {
                        patientDbEntity.Diseases.Remove(diseaseDb);
                    }
                }

                // Amend teeth
                foreach (var tooth in patient.TeethStatus)
                {
                    var toothDb = patientDbEntity.TeethStatus.Find(x => x.Quadrant == tooth.Quadrant && x.OrderNumber == tooth.OrderNumber);
                    toothDb.IsActive = tooth.IsActive;
                    toothDb.StateCode = tooth.StateCode;
                    // Add new procedures
                    for (int i = 0; i < tooth.Procedures.Count; i++)
                    {
                        if (tooth.Procedures[i].Id == 0)
                        {
                            toothDb.Procedures.Add(tooth.Procedures[i]);
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        public int CreatePatient(PatientModel patientModel)
        {
            using (MedicalRecordContext context = new MedicalRecordContext())
            {
                var patient = patientModel.ToPatient();
                var createdPatient = context.Patients.Add(patient);
                context.SaveChanges();
                return createdPatient.Id;
            }
        }
    }
}