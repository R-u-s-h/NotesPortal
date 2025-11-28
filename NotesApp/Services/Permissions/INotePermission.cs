using NotesApp.DbStuff.Models.Notes;

namespace NotesApp.Services.Permissions;

public interface INotePermission
{
    bool IsAllowedToDelete(Note note);
    bool IsAllowedToEdit(Note note);
    bool IsAllowedToTitleUpdate(Note note);
}