using NotesApp.DbStuff.Models.Notes;

namespace NotesApp.DbStuff.Repositories.Interfaces.Notes;

public interface INoteRepository : IBaseDbRepository<Note>
{
    IEnumerable<Note> GetNotesLastWeek();
    IEnumerable<Note>GetNotesByCategoryAsync(int categoryId);
    IEnumerable<Note> GetNotesByTagsAsync(IEnumerable<int> tagIds);
    IEnumerable<Note> GetAllWithAuthor();
    Note GetNoteWithTags(int id);
}