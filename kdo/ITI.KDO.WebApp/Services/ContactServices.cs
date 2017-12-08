using ITI.KDO.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Services
{
    public class ContactServices
    {
        readonly ContactGateway _contactGateway;
        readonly UserGateway _userGateway;

        public ContactServices(ContactGateway contactGateway, UserGateway userGateway)
        {
            _contactGateway = contactGateway;
            _userGateway = userGateway;
        }

        public Result<IEnumerable<Contact>> GetAllByUserId(int userId)
        {
            User user = _userGateway.FindById(userId);
            return Result.Success(Status.Ok, _contactGateway.FindAllByUserId(userId));
        }

        public Result<Contact> GetByIds(int userId, int friendId)
        {
            Contact contact;
            if ((contact = _contactGateway.GetByEmails(firstEmail, secondEmail)) == null) return Result.Failure<Contact>(Status.NotFound, "Contact not found.");
            return Result.Success(Status.Ok, contact);
        }

        public Result<int> Delete(int contactId)
        {
            if (_contactGateway.GetById(contactId) == null) return Result.Failure<int>(Status.NotFound, "Contact not found.");
            _contactGateway.Delete(contactId);
            return Result.Success(Status.Ok, contactId);
        }

        public Result CreateContact(int userId, int friendId, bool invitation)
        {
            if (_userGateway.FindById(userId) == null) return Result.Failure(Status.NotFound, "User not found.");
            if (_userGateway.FindById(friendId) == null) return Result.Failure(Status.NotFound, "User not found.");
            if (_contactGateway.FindByIds(userId, friendId) != null) return Result.Failure(Status.BadRequest, "Contact existed.");

            _contactGateway.CreateContact(userId, friendId, false);
            return Result.Success(Status.Ok);
        }
    }
}
