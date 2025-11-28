using NotesApp.DbStuff.Models.Notes;

namespace NotesApp.DbStuff.Repositories.Interfaces.Notes;

public interface IUserNotesRepository : IBaseDbRepository<User>
{
    bool IsUniqUserName(string userName);
    User? Login(string userName, string password);
    void Registration(string userName, string password);
}