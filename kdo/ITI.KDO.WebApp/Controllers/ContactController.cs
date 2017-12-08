using ITI.KDO.DAL;
using ITI.KDO.WebApp.Authentification;
using ITI.KDO.WebApp.Models.ContactViewModels;
using ITI.KDO.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class ContactController : Controller
    {
        readonly ContactServices _contactService;

        public ContactController(ContactServices ContactService)
        {
            _contactService = ContactService;
        }

        [HttpGet("{ContactId}", Name = "GetContact")]
        public IActionResult GetContactByIds(int UserId, int FriendId)
        {
            Result<Contact> result = _contactService.GetByIds(UserId, FriendId);
            return this.CreateResult<Contact, ContactViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToContactViewModel();
            });
        }

        [HttpPost]
        public IActionResult CreateContact([FromBody] ContactViewModel model)
        {
            Result<Contact> result = _contactService.CreateContact(model.UserId, model.FriendId);
            return this.CreateResult<Contact, ContactViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToContactViewModel();
                o.RouteName = "GetContact";
                o.RouteValues = s => new { userId = s.UserId, friendId = s.FriendId };
            });
        }

        [HttpDelete("{userId}/{friendId}")]
        public IActionResult DeleteContact(int userId, int friendId)
        {
            Result<int> result = _contactService.Delete(userId, friendId);
            return this.CreateResult(result);
        }

        [HttpGet("{userId}/getContactsByUserId")]
        public IActionResult GetContactList(int userId)
        {
            Result<IEnumerable<Contact>> result = _contactService.GetAllByUserId(userId);
            return this.CreateResult<IEnumerable<Contact>, IEnumerable<ContactViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToContactViewModel());
            });
        }
    }
}
