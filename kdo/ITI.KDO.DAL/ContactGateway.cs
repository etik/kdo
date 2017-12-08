using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ITI.KDO.DAL
{
    public class ContactGateway
    {
        readonly string _connectionString;

        public ContactGateway(string connectionString)
        {
            _connectionString = connectionString;
        }
        /// <summary>
        /// Add a Contact
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="friendId"></param>
        /// <param name="invitation"></param>
        public void Create(int userId, int friendId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sContactCreate",
                    new
                    {
                        UserId = userId,
                        FriendId = friendId,
                        Invitation = true
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
        /// <summary>
        /// Find contact by the UserId, FriendId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="friendId"></param>
        /// <returns></returns>
        public Contact FindByIds(int userId, int friendId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Contact>(
                    @"select c.UserId,
                             c.FriendId,
                             c.Invitation
                      from dbo.vContact c 
                      where c.UserId = @UserId and
                            c.FriendId = @FriendId",
                    new { UserId = userId, friendId = friendId })
                    .FirstOrDefault();
            }
        }

        public IEnumerable<Contact> GetAllByUserId(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Contact>(
                    @"select e.UserId,
                             e.FriendId,
                             e.Invitation
                      from dbo.vContact e 
                      where e.UserId = @UserId;",
                    new { UserId = userId });
            }
        }

        public void Delete(int userId, int friendId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sContactDelete",
                    new { UserId = userId, FriendId = friendId },
                    commandType: CommandType.StoredProcedure);
            }
        }

    }
}
