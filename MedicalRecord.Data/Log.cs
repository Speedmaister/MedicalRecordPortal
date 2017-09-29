using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecord.Data
{
    [Table("Log")]
    public class Log
    {
        /*CREATE TABLE [dbo].[Log] (
    [Id] [int] IDENTITY (1, 1) NOT NULL,
    [Date] [datetime] NOT NULL,
    [Thread] [varchar] (255) NOT NULL,
    [Level] [varchar] (50) NOT NULL,
    [Logger] [varchar] (255) NOT NULL,
    [Message] [varchar] (4000) NOT NULL,
    [Exception] [varchar] (2000) NULL
)*/
        [Key]
        public int Id { get; set; }
        
        public DateTime Date { get; set; }

        [StringLength(255)]
        [Required]
        public string Thread { get; set; }

        [StringLength(50)]
        [Required]
        public string Level { get; set; }

        [StringLength(255)]
        [Required]
        public string Logger { get; set; }

        [StringLength(4000)]
        [Required]
        public string Message { get; set; }

        [StringLength(4000)]
        [Required]
        public string Exception { get; set; }
    }
}
