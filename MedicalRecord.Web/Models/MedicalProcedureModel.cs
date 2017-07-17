using MedicalRecord.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalRecord.Web.Models
{
    public class MedicalProcedureModel
    {
        public MedicalProcedureModel() { }

        public MedicalProcedureModel(MedicalProcedure procedureEntity, int toothId)
        {
            Id = procedureEntity.Id;
            Date = procedureEntity.Date;
            Diagnose = procedureEntity.Diagnose;
            Name = procedureEntity.Name;
            Price = procedureEntity.Price;
            Notes = procedureEntity.Notes;
            ToothId = toothId;
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string Diagnose { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Notes { get; set; }

        public int ToothId { get; set; }
    }
}