using ProgramPartListWeb.Areas.PC.Interface;
using ProgramPartListWeb.Areas.PC.Models;
using ProgramPartListWeb.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.PC.Repository
{
    public class NotificationRepository : INotification
    {
        public Task<List<Notification>> GetUserNotifications()
        {
            return SqlDataAccess.GetData<Notification>("SELECT NotificationID, Title, Message, IsRead FROM Patrol_Notification");
        }
       
        public Task<bool> MarkAsRead(int NotID)
        {
            string strsql = "UPDATE Patrol_Notification SET IsRead = @IsRead WHERE NotificationID =@NotificationID";
            return SqlDataAccess.UpdateInsertQuery(strsql, new { IsRead = 1, NotificationID = NotID });
        }
        public Task<bool> AddNotification(Notification not)
        {
            string strsql = "INSERT INTO Patrol_Notification(Title, Message) VALUES (@Title, @Message)";
            return SqlDataAccess.UpdateInsertQuery(strsql, new { Title = not.Title, Message = not.Message });
        }
    }
}