using System.ComponentModel.DataAnnotations;

namespace NotesApp.Models.Notes;

public class CategoryViewModel
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
}