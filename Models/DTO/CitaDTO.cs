namespace medicalAppointmentsAPI.Models.DTO
{
    public class CitaDTO
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Reason { get; set; }

        public int Attribute11 { get; set; }

        public int Patient_id { get; set; }

        public int diagnostico_id { get; set; }
    }
}
