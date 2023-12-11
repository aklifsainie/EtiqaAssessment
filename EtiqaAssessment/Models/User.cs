using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EtiqaAssessment.Models
{
    public class User
    {
        [Required]
        [Key]
        [Column(TypeName = "nvarchar")]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Mail { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string? Skillset { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string? Hobby { get; set; }
    }
}
