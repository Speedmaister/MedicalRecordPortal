using System.ComponentModel.DataAnnotations;

namespace MedicalRecord.Data
{
    public class ToothState
    {
        [Key]
        [MaxLength(3)]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
    }
}