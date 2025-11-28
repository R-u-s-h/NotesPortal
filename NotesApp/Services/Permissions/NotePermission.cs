using NotesApp.DbStuff.Models.Notes;
using NotesApp.Enum;

namespace NotesApp.Services.Permissions;

public class NotePermission : INotePermission
{
    private IAuthNotesService _authNotesService;

    public NotePermission(IAuthNotesService authNotesService)
    {
        _authNotesService = authNotesService;
    }

    public bool IsAllowedToDelete(Note note)
    {
        if (!_authNotesService.IsAuthenticated())
        {
            return false;
        }

        var user = _authNotesService.GetUser();
        if (user.Role == NotesUserRole.Administrator
            || user.Role == NotesUserRole.Moderator)
        {
            return true;
        }

        return note.Author?.Id == user.Id;
    }

    public bool IsAllowedToEdit(Note note)
    {
        if (!_authNotesService.IsAuthenticated())
        {
            return false;
        }

        var user = _authNotesService.GetUser();
        if (user.Role == NotesUserRole.Administrator
            || user.Role == NotesUserRole.Moderator)
        {
            return true;
        }

        return note.Author?.Id == user.Id;
    }
    
    public bool IsAllowedToTitleUpdate(Note note)
    {
        if (!_authNotesService.IsAuthenticated())
        {
            return false;
        }

        var user = _authNotesService.GetUser();
        if (user.Role == NotesUserRole.Administrator
            || user.Role == NotesUserRole.Moderator)
        {
            return true;
        }

        return note.Author?.Id == user.Id;
    }
}