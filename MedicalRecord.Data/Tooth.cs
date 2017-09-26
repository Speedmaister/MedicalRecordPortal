using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalRecord.Data
{
    [Table("Teeth")]
    public class Tooth
    {
        public Tooth()
        {
            Procedures = new List<MedicalProcedure>();
        }

        public int Id { get; set; }

        public byte Quadrant { get; set; }

        public byte OrderNumber { get; set; }

        public bool IsActive { get; set; }

        public ToothTypes Type { get; set; }

        [Required]
        [ForeignKey(nameof(State))]
        public string StateCode { get; set; }

        public virtual ToothState State { get; set; }

        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual List<MedicalProcedure> Procedures { get; set; }

        public override string ToString()
        {
            return $"Id:{Id} Quadrant:{(int)Quadrant} OrderNumber:{OrderNumber} IsActive:{IsActive} State:{State}";
        }
    }
}