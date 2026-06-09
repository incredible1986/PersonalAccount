using System.ComponentModel.DataAnnotations;

namespace PersonalAccount.ViewModels;

public class AddDisciplineViewModel : ViewModel
{
    [Required(ErrorMessage = "Название дисциплины обязательно")]
    public string Name { get; set; } = string.Empty;
}