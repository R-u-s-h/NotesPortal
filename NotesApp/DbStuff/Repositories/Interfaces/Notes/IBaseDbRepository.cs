using NotesApp.DbStuff.Models;

namespace NotesApp.DbStuff.Repositories.Interfaces.Notes;

public interface IBaseDbRepository<TDbModel> where TDbModel : BaseModel
{
    TDbModel Add(TDbModel model);
    TDbModel GetFirstById(int id);
    List<TDbModel> GetAll();
    void Remove(TDbModel model);
    void Remove(int id);
    void Update(TDbModel model);
}