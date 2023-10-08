using System.ComponentModel.DataAnnotations;

namespace ContactBookAPI.Model.ViewModel
{
    public class PutViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(PhoneNumber))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
