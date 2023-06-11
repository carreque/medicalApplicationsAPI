namespace medicalAppointmentsAPI.Models.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        public string name { get; set; }

        public string lastnames { get; set; }

        public string user { get; set; }

        public string key { get; set; }

        public string Discriminator { get; set; }

        public string? NSS { get; set; } = null;

        public string? cardNumber { get; set; } = null;
        public string? tlf { get; set; } = null;

        public string? address { get; set; } = null;

        public string? MemberShipNumber { get; set; } = null;

        public string? Password { get; set; } = null;

        public byte[]? PasswordHash { get; set; } = null;
        public byte[]? PasswordSalt { get; set; } = null;
    }
}
