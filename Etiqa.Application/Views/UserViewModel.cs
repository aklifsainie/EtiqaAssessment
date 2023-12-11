using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Etiqa.Application.Views
{
    public class UserViewModel
    {
        [Required]
        [Key]
        [Column(TypeName = "nvarchar")]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [Display(Name ="Email")]
        public string Mail { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string? Skillset { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string? Hobby { get; set; }
    }
}
