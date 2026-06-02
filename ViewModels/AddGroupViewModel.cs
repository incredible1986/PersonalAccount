using System.ComponentModel.DataAnnotations;

namespace PersonalAccount.ViewModels;

public class AddGroupViewModel : ViewModel
{
    [Required(ErrorMessage = "Название группы обязательно")]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }
}