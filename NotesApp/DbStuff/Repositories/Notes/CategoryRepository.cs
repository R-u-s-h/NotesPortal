using NotesApp.DbStuff.Models.Notes;
using NotesApp.DbStuff.Repositories.Interfaces.Notes;

namespace NotesApp.DbStuff.Repositories.Notes;

public class CategoryRepository : BaseDbRepository<Category>, ICategoryRepository
{
    public CategoryRepository(NotesDbContext notesDbContext) : base(notesDbContext)
    {
    }
}