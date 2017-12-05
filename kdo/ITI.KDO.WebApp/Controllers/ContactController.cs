using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ITI.KDO.WebApp.Authentification;
using ITI.KDO.WebApp.Services;
using ITI.KDO.DAL;
using ITI.KDO.WebApp.Models.UserViewModels;

namespace ITI.KDO.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class ContactController : Controller
    {
        readonly ContactServices _contactServices;

        public ContactController(ContactServices contactServices)
        {
            _contactServices = contactServices;
        }

        [HttpPost]
        public IActionResult CreateContact([FromBody] ContactViewModel model)
        {
            Result result = _contactServices.CreateContact(model.FirstEmail, model.SecondEmail);
            return this.CreateResult(result);
        }

        [HttpGet("{firstEmail}/{secondEmail}")]
        public IActionResult GetContactByEmails(string firstEmail, string secondEmail)
        {
            Result<Contact> result = _contactServices.GetByEmails(firstEmail, secondEmail);
            return this.CreateResult<Contact, ContactViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToContactViewModel();
            });
        }

        [HttpDelete("{contactId}")]
        public IActionResult DeleteContact(int contactId)
        {
            Result<int> result = _contactServices.Delete(contactId);
            return this.CreateResult(result);
        }
    }
}