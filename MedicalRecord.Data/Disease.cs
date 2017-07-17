using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecord.Data
{
    public class Disease
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }
        
        public virtual Patient Patient { get; set; }
    }
}
