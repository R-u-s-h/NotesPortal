using System.ComponentModel.DataAnnotations;

namespace NotesApp.Models.Notes;

public class CategoryViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    public bool IsAllowedToDelete { get; set; }
}