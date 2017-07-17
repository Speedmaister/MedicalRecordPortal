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
    }
}