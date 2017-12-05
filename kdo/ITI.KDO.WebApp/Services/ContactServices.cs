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

        public Result<IEnumerable<Contact>> GetAllByEmail(string email)
        {
            return Result.Success(Status.Ok, _contactGateway.GetAllByEmail(email));
        }

        public Result<Contact> GetByEmails(string firstEmail, string secondEmail)
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

        public Result CreateContact(string firstEmail, string secondEmail)
        {
            if (string.IsNullOrEmpty(firstEmail)) return Result.Failure(Status.BadRequest, "The first person's email is not valid.");
            if (string.IsNullOrEmpty(secondEmail)) return Result.Failure(Status.BadRequest, "The second person's email is not valid.");
            if (_contactGateway.GetByEmails(firstEmail, secondEmail) != null) return Result.Failure(Status.NotFound, "Contact already exist.");

            _contactGateway.Create(firstEmail, secondEmail);
            return Result.Success(Status.Ok);

        }
    }
}
