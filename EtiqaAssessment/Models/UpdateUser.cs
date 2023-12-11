namespace EtiqaAssessment.Models
{
    public class UpdateUser
    {
        public string Username { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }

        public string? Skillset { get; set; }

        public string? Hobby { get; set; }
    }
}
