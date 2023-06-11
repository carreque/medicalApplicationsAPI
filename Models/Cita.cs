using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medicalAppointmentsApplication.Models
{
    public class Cita
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public int Attribute11 { get; set; }

        [Required]
        public int Patient_id { get; set; }

        public int diagnostico_id { get; set; }

        /*public int getAttribute11()
        {
            return this.Attribute11;
        }

        public Cita setNewAttribute11(int newAttribute11)
        {
            this.Attribute11 = newAttribute11;
            return this;
        }*/
    }
}
