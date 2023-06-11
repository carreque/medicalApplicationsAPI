using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medicalAppointmentsApplication.Models
{
    public class Paciente : Usuario
    {
        [Required]
        public string NSS { get; set; }

        [Required]
        public string cardNumber { get; set; }
        [Required]
        public string tlf { get; set; }
        [Required]
        public string address { get; set; }

    }
}
