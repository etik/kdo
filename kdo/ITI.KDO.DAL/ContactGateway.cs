using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;

namespace ITI.KDO.DAL
{
    public class ContactGateway
    {
        readonly string _connectionString;
        
        public ContactGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Contact> GetAllByEmail(string email)
        {
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Contact>(
                    @"select c.ContactId,
                             c.FirstEmail,
                             c.SecondEmail
                    from dbo.vContact c
                    where c.FirstEmail = @UserEmail or
                          c.SecondEmail = @UserEmail;",
                    new { UserEmail = email});
            }
        }

        /// <summary>
        /// Get contact by user Id
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public Contact GetById(int contactId)
        {
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Contact>(
                    @"select c.ContactId
                             c.FirstEmail,
                             c.SecondEmail
                    from dbo.vContact c
                    where c.ContactId = @ContactId;",
                    new { ContactId = contactId })
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Get contact by emails of 2 users
        /// </summary>
        /// <param name="firstEmail"></param>
        /// <param name="secondEmail"></param>
        /// <returns></returns>
        public Contact GetByEmails(string firstEmail, string secondEmail)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return con.Query<Contact>(
                    @"select c.ContactId,
                             c.FirstEmail,
                             c.SecondEmail
                    from dbo.vContact c
                    where (c.FirstEmail = @FirstEmail and c.SecondEmail = @SecondEmail) or
                          (c.SecondEmail = @FirstEmail and c.FirstEmail = @SecondEmail);",
                    new { FirstEmail = firstEmail, SecondEmail = secondEmail })
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Delete contact
        /// </summary>
        /// <param name="contactId"></param>
        public void Delete(int contactId)
        {
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sContactDelete",
                    new { ContactId = contactId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Create a Contact
        /// </summary>
        /// <param name="firstEmail"></param>
        /// <param name="secondEmail"></param>
        public void Create(string firstEmail, string secondEmail)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Execute(
                    "dbo.sContactCreate",
                    new
                    {
                        FirstEmail = firstEmail,
                        SecondEmail = secondEmail
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

    }
}
