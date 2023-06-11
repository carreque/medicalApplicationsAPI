using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medicalAppointmentsApplication.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string lastnames { get; set; }

        [Required]
        public string user { get; set; }

        [Required]
        public string key { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        /*public string getKey()
        {
            return this.key;
        }

        public Usuario setKey(string newKey)
        {
            this.key = newKey;
            return this;
        }*/

    }
}
