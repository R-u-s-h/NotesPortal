using NotesApp.DbStuff.Models.Notes;
using NotesApp.DbStuff.Repositories.Interfaces.Notes;

namespace NotesApp.DbStuff.Repositories.Notes;

public class TagRepository : BaseDbRepository<Tag>, ITagRepository
{
    public TagRepository(NotesDbContext notesDbContext) : base(notesDbContext)
    {
    }
    
    public List<string> GetTagNamesByIds(List<int> tagIds)
    {
        return _dbSet
            .Where(t => tagIds.Contains(t.Id))
            .Select(t => t.Name)
            .ToList();
    }
}