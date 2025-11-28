using NotesApp.DbStuff.Models.Notes;
using NotesApp.Enum;

namespace NotesApp.Services;

public interface IAuthNotesService
{
    int GetId();
    User GetUser();
    bool IsAuthenticated();
    Language GetLanguage();
    NotesUserRole GetRole();
    string GetName();
    bool IsAdmin();
    Task SignInUser(User user);
}