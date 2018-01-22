using ITI.KDO.DAL;
using ITI.KDO.WebApp.Authentification;
using ITI.KDO.WebApp.Services;
using ITI.KDO.WebApp.Models.EventViewModels;
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
    public class EventController: Controller
    {
        readonly EventServices _eventService;
        public EventController(EventServices eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("{userId}/getEventByUserId")]
        public IActionResult GetEventList(int userId)
        {
            Result<IEnumerable<Event>> result = _eventService.GetAllByUserId(userId);


            return this.CreateResult<IEnumerable<Event>, IEnumerable<EventViewModel>>(result, o =>
            {
                o.ToViewModel = x => x.Select(s => s.ToEventViewModel());
            });
        }

        [HttpGet("{eventId}", Name = "GetEvent")]
        public IActionResult GetEventById(int eventId)
        {
            Result<Event> result = _eventService.GetById(eventId);
            return this.CreateResult<Event, EventViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToEventViewModel();
            });
        }

        [HttpGet("{eventId}/{userId}/getEvent")]
        public IActionResult GetEventByUserIdEventId(int eventId, int userId)
        {
            Result<Event> result = _eventService.GetByIds(userId, eventId);
            return this.CreateResult<Event, EventViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToEventViewModel();
            });
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody] EventViewModel model)
        {
            Result<int> result = _eventService.CreateEvent(model.UserId, model.EventName, model.Descriptions, model.Dates);
            return this.CreateResult(result);
        }

        [HttpPut("{eventId}")]
        public IActionResult UpdateEvent(int eventId, [FromBody] EventViewModel model)
        {
            Result<Event> result = _eventService.UpdateEvent(model.EventId, model.UserId, model.EventName, model.Descriptions, model.Dates);
            return this.CreateResult<Event, EventViewModel>(result, o =>
            {
                o.ToViewModel = s => s.ToEventViewModel();
            });
        }

        [HttpDelete("{eventId}")]
        public IActionResult DeleteEvent(int eventId)
        {
            Result<int> result = _eventService.Delete(eventId);
            return this.CreateResult(result);
        }
    }
}
