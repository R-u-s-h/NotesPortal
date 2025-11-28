using NotesApp.Enum;

namespace NotesApp.Models.Notes;

public class ProfileNotesViewModel
{
    public List<Language> Languages { get; set; }
    public Language Language { get; set; }
    public string AvatarUrl { get; set; }
    public string Name { get; set; }
}