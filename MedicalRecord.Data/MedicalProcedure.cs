using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalRecord.Data
{
    public class MedicalProcedure
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Diagnose { get; set; }

        [Required]
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public string Notes { get; set; }
        
        [ForeignKey(nameof(Tooth))]
        public int ToothId { get; set; }

        public virtual Tooth Tooth { get; set; }
    }
}