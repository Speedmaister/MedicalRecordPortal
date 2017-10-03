using MedicalRecord.Data;
using MedicalRecord.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalRecord.Web.DataPersisters
{
    public class ToothPersister
    {
        public List<ToothStateModel> GetToothStates()
        {
            using (MedicalRecordContext context = new MedicalRecordContext())
            {
                var stateEntities = context.ToothStates.ToList();
                return stateEntities.Select(x => new ToothStateModel(x)).ToList();
            }
        }

        public void RemoveProcedure(int procedureId)
        {
            using (MedicalRecordContext context = new MedicalRecordContext())
            {
                var procedure = context.MedicalProcedures.Find(procedureId);
                if(procedure != null)
                {
                    context.MedicalProcedures.Remove(procedure);
                    context.SaveChanges();
                }
            }
        }
    }
}