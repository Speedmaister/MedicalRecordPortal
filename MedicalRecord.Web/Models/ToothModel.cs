using MedicalRecord.Data;
using System.ComponentModel.DataAnnotations;

namespace MedicalRecord.Web.Models
{
    public class ToothModel
    {
        public ToothModel() { }

        public ToothModel(Tooth toothEntity)
        {
            Id = toothEntity.Id;
            Quadrant = (Quadrants)toothEntity.Quadrant;
            OrderNumber = toothEntity.OrderNumber;
            Type = toothEntity.Type;
            StateCode = toothEntity.StateCode;
            IsActive = toothEntity.IsActive;
        }

        public ToothModel(Quadrants quadrant, ToothTypes type, byte orderNumber)
        {
            Quadrant = quadrant;
            Type = type;
            OrderNumber = orderNumber;
            StateCode = "N";
        }

        public int Id { get; set; }

        public Quadrants Quadrant { get; set; }

        public byte OrderNumber { get; set; }

        public bool IsActive { get; set; }

        public ToothTypes Type { get; set; }

        [Required]
        public string StateCode { get; set; }

        public int Number
        {
            get
            {
                return (int)Quadrant * 10 + OrderNumber;
            }
        }
    }
}