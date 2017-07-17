using MedicalRecord.Data;

namespace MedicalRecord.Web.Models
{
    public class ToothStateModel
    {
        public ToothStateModel(ToothState state)
        {
            Code = state.Code;
            Name = state.Name;
        }

        public string Code { get; set; }
        public string Name { get; set; }
    }
}