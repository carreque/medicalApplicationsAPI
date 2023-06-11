using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medicalAppointmentsApplication.Models
{
    public class Diagnostico
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ExpertAssestment { get; set; }

        [Required]
        public string Disease { get; set; }

        [Required]
        public int cita_id { get; set; }
    }
}
