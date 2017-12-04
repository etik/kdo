using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Linq;

namespace ITI.KDO.DAL
{
    public class NotificationGateway
    {
        readonly string _connectionString;

        public NotificationGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Notification> GetAllByUserId(int userId)
        {
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Notification>(
                    @"select n.NotificationId,
                             n.UserId,
                             n.RecipientsEmail,
                             n.SenderEmail,
                             n.Descriptions,
                             n.InviteAccept
                      from dbo.vNotification n
                      where n.UserId = @UserId;",
                    new { UserId = userId });
            }
        }

        public void Create(int userId, string recipientsEmail, string senderEmail, string descriptions, bool inviteAccept)
        {
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sNotificationCreate",
                    new
                    {
                        UserId = userId,
                        RecipientsEmail = recipientsEmail,
                        SenderEmail = senderEmail,
                        Descriptions = descriptions,
                        InviteAccept = inviteAccept
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        public void Update(int notificationId, int userId, string recipientsEmail, string senderEmail, string descriptions, bool inviteAccept)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sNotificationUpdate",
                    new
                    {
                        NotificationId = notificationId,
                        UserId = userId,
                        RecipientsEmail = recipientsEmail,
                        SenderEmail = senderEmail,
                        Descriptions = descriptions,
                        InviteAccept = inviteAccept
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public Notification FindByNotificationId(int notificationId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Notification>(
                        @"select n.NotificationId,
                             n.UserId,
                             n.RecipientsEmail,
                             n.SenderEmail,
                             n.Descriptions,
                             n.InviteAccept
                      from dbo.vNotification n
                      where n.NotificationId = @NotificationId;",
                        new { NotificationId = notificationId })
                    .FirstOrDefault();
            }
        }

        public void Delete(int notificationId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sNotificationDelete",
                    new { NotificationId = notificationId },
                    commandType: CommandType.StoredProcedure);
            }
        }

    }
}
