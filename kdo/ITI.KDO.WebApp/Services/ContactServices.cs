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

        public ContactServices(ContactGateway contactGateway)
        {
            _contactGateway = contactGateway;
        }

        public Result<Contact> CreateContact(int userId, int friendId)
        {
            _contactGateway.Create(userId, friendId);
            Contact contact = _contactGateway.FindByIds(userId, friendId);
            return Result.Success(Status.Ok, contact);
        }

        public Result<Contact> GetByIds(int userId, int friendId)
        {
            Contact contact;
            if ((contact = _contactGateway.FindByIds(userId, friendId)) == null) return Result.Failure<Contact>(Status.NotFound, "Event not found.");
            return Result.Success(Status.Ok, contact);
        }

        public Result<int> Delete(int userId, int friendId)
        {
            if (_contactGateway.FindByIds(userId, friendId) == null) return Result.Failure<int>(Status.NotFound, "Event not found.");
            _contactGateway.Delete(userId, friendId);
            return Result.Success(Status.Ok, userId);
        }

        public Result<IEnumerable<Contact>> GetAllByUserId(int userId)
        {
            return Result.Success(Status.Ok, _contactGateway.GetAllByUserId(userId));
        }
    }
}
