using MedicalRecord.Data;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalRecord.Web.Models
{
    public class PatientModel
    {
        public const int PermanentTeethCountInQuadrant = 8;
        public const int PrimaryTeethCountInQuadrant = 5;

        public PatientModel()
            : this(false, null)
        {
        }

        public PatientModel(Patient patientEntity)
            : this(true, patientEntity.TeethStatus)
        {
            Id = patientEntity.Id;
            FirstName = patientEntity.FirstName;
            SurName = patientEntity.SurName;
            LastName = patientEntity.LastName;
            EGN = patientEntity.EGN;
            Telephone = patientEntity.Telephone;
            Address = patientEntity.Address;

            for (int i = 0; i < patientEntity.Diseases.Count; i++)
            {
                Diseases.Add(new DiseaseModel(patientEntity.Diseases[i]));
            }
        }

        private PatientModel(bool alreadyHasTeeth, List<Tooth> teeth)
        {
            Diseases = new List<DiseaseModel>();
            Procedures = new List<MedicalProcedureModel>();
            TeethStatus = new Dictionary<Quadrants, ToothModel[]>();

            if (alreadyHasTeeth)
            {
                LoadTeethStatus(teeth);
            }
            else
            {
                InitializeTeethStatus();
            }
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Име:")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Фамилия:")]
        public string LastName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Презиме:")]
        public string SurName { get; set; }

        public string FullName
        {
            get
            {
                return string.IsNullOrWhiteSpace(SurName) ? $"{FirstName} {LastName}" : $"{FirstName} {SurName} {LastName}";
            }
        }

        [Required]
        [MaxLength(15)]
        [Display(Name = "ЕГН:")]
        public string EGN { get; set; }

        [Required]
        [MaxLength(15)]
        [Display(Name = "Телефон:")]
        public string Telephone { get; set; }

        [MaxLength(500)]
        [Display(Name = "Адрес:")]
        public string Address { get; set; }

        [Display(Name = "Заболявания:")]
        public List<DiseaseModel> Diseases { get; set; }

        [Display(Name = "Зъбен статус:")]
        public Dictionary<Quadrants, ToothModel[]> TeethStatus { get; set; }

        [Display(Name = "Процедури:")]
        public List<MedicalProcedureModel> Procedures { get; set; }

        private void InitializeTeethStatus()
        {
            // Initialize 1-4 Quadrants
            InitializeQuadrant(1, 4, PermanentTeethCountInQuadrant, ToothTypes.Permanent);

            // Initialize 5-8 Quadrants
            InitializeQuadrant(5, 8, PrimaryTeethCountInQuadrant, ToothTypes.Primary);

            // Activate 1-4 Quadrants
            TeethStatus[Quadrants.First]
                .Union(TeethStatus[Quadrants.Second])
                .Union(TeethStatus[Quadrants.Third])
                .Union(TeethStatus[Quadrants.Fourth])
                .ToList().ForEach(x => x.IsActive = true);
        }

        private void InitializeQuadrant(int startQuadrant, int endQuadrant, int teethCountInQuadrant, ToothTypes teethType)
        {
            for (int i = startQuadrant; i <= endQuadrant; i++)
            {
                var quadrant = (Quadrants)i;
                var quadrantOfTeeth = new ToothModel[teethCountInQuadrant];
                TeethStatus[quadrant] = quadrantOfTeeth;
                for (byte j = 0; j < quadrantOfTeeth.Length; j++)
                {
                    quadrantOfTeeth[j] = new ToothModel(quadrant, teethType, (byte)(j + 1));
                }
            }
        }

        private void LoadTeethStatus(List<Tooth> teeth)
        {
            // Initialize 1-4 Quadrants
            LoadQuadrant(teeth, 1, 4, PermanentTeethCountInQuadrant);

            // Initialize 5-8 Quadrants
            LoadQuadrant(teeth, 5, 8, PrimaryTeethCountInQuadrant);
        }

        private void LoadQuadrant(List<Tooth> teeth, int startQuadrant, int endQuadrant, int teethCountInQuadrant)
        {
            for (int i = startQuadrant; i <= endQuadrant; i++)
            {
                var quadrantOfTeeth = new ToothModel[teethCountInQuadrant];
                TeethStatus[(Quadrants)i] = quadrantOfTeeth;
                for (int j = 0; j < quadrantOfTeeth.Length; j++)
                {
                    var tooth = teeth.FirstOrDefault(x => x.Quadrant == i && x.OrderNumber == (j + 1));
                    quadrantOfTeeth[j] = new ToothModel(tooth);
                    for (int k = 0; k < tooth.Procedures.Count; k++)
                    {
                        Procedures.Add(new MedicalProcedureModel(tooth.Procedures[j]));
                    }
                }
            }
        }

        public Patient ToPatient()
        {
            Patient patient = new Patient()
            {
                Id = this.Id,
                FirstName = this.FirstName,
                SurName = this.SurName,
                LastName = this.LastName,
                EGN = this.EGN,
                Telephone = this.Telephone,
                Address = this.Address,
                Diseases = this.Diseases.Select(x => new Disease() { Id = x.Id, Name = x.Name }).ToList()
            };

            foreach (var quadrant in TeethStatus)
            {
                foreach(var toothModel in quadrant.Value)
                {
                    var tooth = new Tooth();
                    tooth.Id = toothModel.Id;
                    tooth.Type = toothModel.Type;
                    tooth.OrderNumber = toothModel.OrderNumber;
                    tooth.Quadrant = (byte)toothModel.Quadrant;
                    tooth.StateCode = toothModel.StateCode;
                    tooth.IsActive = toothModel.IsActive;
                    tooth.Procedures = new List<MedicalProcedure>();

                    foreach (var procedureModel in Procedures.Where(x => x.ToothNumber == toothModel.Number))
                    {
                        MedicalProcedure procedure = new MedicalProcedure();
                        procedure.Id = procedureModel.Id;
                        procedure.Name = procedureModel.Name;
                        procedure.Price = procedureModel.Price;
                        procedure.ToothId = toothModel.Id;
                        procedure.Notes = procedureModel.Notes;
                        procedure.Diagnose = procedureModel.Diagnose;
                        procedure.Date = procedureModel.Date;

                        tooth.Procedures.Add(procedure);
                    }

                    patient.TeethStatus.Add(tooth);
                }
            }

            return patient;
        }
    }
}