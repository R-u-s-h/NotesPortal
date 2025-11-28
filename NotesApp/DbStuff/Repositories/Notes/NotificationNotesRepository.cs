using Microsoft.EntityFrameworkCore;
using NotesApp.DbStuff.Models.Notes;
using NotesApp.DbStuff.Repositories.Interfaces.Notes;

namespace NotesApp.DbStuff.Repositories.Notes;

public class NotificationNotesRepository : BaseDbRepository<NotificationNotes>, INotificationNotesRepository
{
    public NotificationNotesRepository(NotesDbContext notesDbContext) : base(notesDbContext)
    {
    }

    public NotificationNotes GetByIdWithUsers(int notificationId)
    {
        return _dbSet
            .Include(x => x.UserWhoViewedIt)
            .First(x => x.Id == notificationId);
    }

    public List<NotificationNotes> GetNewNotificationForMe(int userId)
    {
        var lastWeek = DateTime.Now.AddDays(-7);
        return _dbSet
            .Where(notification =>
                !notification.UserWhoViewedIt.Select(u => u.Id).Contains(userId)
                && notification.CreateAt > lastWeek)
            .ToList();
    }
}