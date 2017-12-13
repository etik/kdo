using ITI.KDO.WebApp.Authentification;
using Microsoft.AspNetCore.Authorization;
using ITI.KDO.WebApp.Models.ParticipantViewModels;
using Microsoft.AspNetCore.Mvc;
using ITI.KDO.DAL;
using System;
using ITI.KDO.WebApp.Services;
using ITI.KDO.WebApp.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]

    public class ParticipantController: Controller
    {
        readonly ParticipantServices _participantService;

        public ParticipantController(ParticipantServices participantService)
        {
            _participantService = participantService;
        }

        [HttpGet("{userId}/{eventId}/getParticipantByUserId")]
        public IActionResult GetParticipantList(int userId, int eventId)
        {
            Result<IEnumerable<Participant>> result = _participantService.FindById(userId, eventId);
            return this.CreateResult<IEnumerable<Participant>, IEnumerable<ParticipantViewModel>>(result, o =>
             {
                 o.ToViewModel = x => x.Select(s => s.ToParticipantViewModel());
             });
        }
    }
}
