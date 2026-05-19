using System.ComponentModel.DataAnnotations;

namespace PersonalAccount.Models
{
    public class StudentEditViewModel
    {
        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Группа обязательна для заполнения")]
        public string GroupName { get; set; } = string.Empty;

        public string? PhotoUrl { get; set; }
    }
}