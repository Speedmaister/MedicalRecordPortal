using System.ComponentModel.DataAnnotations;

namespace MedicalRecord.Data
{
    public class ToothState
    {
        [Key]
        [MaxLength(2)]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
    }
}