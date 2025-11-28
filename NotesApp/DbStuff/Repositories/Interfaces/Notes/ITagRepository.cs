using NotesApp.DbStuff.Models.Notes;

namespace NotesApp.DbStuff.Repositories.Interfaces.Notes;

public interface ITagRepository : IBaseDbRepository<Tag>
{
    List<string> GetTagNamesByIds(List<int> tagIds);
}