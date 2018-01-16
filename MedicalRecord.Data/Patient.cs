using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecord.Data
{
    public class Patient
    {
        public Patient()
        {
            Diseases = new List<Disease>();
            TeethStatus = new List<Tooth>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string SurName { get; set; }

        [Required]
        [MaxLength(15)]
        public string EGN { get; set; }

        [Required]
        [MaxLength(15)]
        public string Telephone { get; set; }
        
        [MaxLength(500)]
        public string Address { get; set; }
        
        public bool IsDeleted { get; set; }

        public virtual List<Disease> Diseases { get; set; }

        public virtual List<Tooth> TeethStatus { get; set; }
    }
}