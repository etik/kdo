using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ITI.KDO.DAL
{
    public class ParticipantGateway
    {
        readonly string _connectionString;

        public ParticipantGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Create Participant
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <param name="participantType"></param>
        public void Create(int userId, int eventId, Byte participantType)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sParticipantCreate",
                    new
                    {
                        UserId = userId,
                        EventId = eventId,
                        ParticipantType = participantType
                        
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Find a participant with the userId and eventId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Participant FindById(int userId, int eventId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Participant>(
                    @"select p.UserId,
                             p.EventId,
                             p.ParticipantType    
                  from dbo.vParticipant p
                    where p.UserId = @UserId and p.EventId = @EventId",
                    new { UserId = userId, EventId = eventId })
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Delete a participant with userId and eventId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        public  void Delete(int userId, int eventId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sParticipantDelete",
                    new { UserId = userId, EventId = eventId },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
