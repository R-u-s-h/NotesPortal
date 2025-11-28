using NotesApp.DbStuff.Models.Notes;

namespace NotesApp.DbStuff.Repositories.Interfaces.Notes;

public interface INotificationNotesRepository : IBaseDbRepository<NotificationNotes>
{
    NotificationNotes GetByIdWithUsers(int notificationId);
    List<NotificationNotes> GetNewNotificationForMe(int userId);
}