using ProgramPartListWeb.Areas.PC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.PC.Interface
{
    public interface INotification
    {
        Task<List<Notification>> GetUserNotifications();
        Task<bool> MarkAsRead(int NotID);
        Task<bool> AddNotification(Notification not);
    }
}
