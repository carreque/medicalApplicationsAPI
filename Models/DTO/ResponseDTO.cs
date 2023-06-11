namespace medicalAppointmentsAPI.Models.DTO
{
    public class ResponseDTO
    {
        public bool isSuccess { get; set; } = true;

        public object Result { get; set; }

        public string MessageDisplay { get; set; }

        public List<string> ErrorMessages { get; set; }
    }
}
