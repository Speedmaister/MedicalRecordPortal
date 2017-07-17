using MedicalRecord.Data;
using System.ComponentModel.DataAnnotations;

namespace MedicalRecord.Web.Models
{
    public class DiseaseModel
    {
        public DiseaseModel(Disease diseaseEntity)
        {
            Id = diseaseEntity.Id;
            Name = diseaseEntity.Name;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}