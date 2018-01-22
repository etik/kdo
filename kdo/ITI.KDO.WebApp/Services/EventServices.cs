using ITI.KDO.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Services
{
    public class EventServices
    {
        readonly EventGateway _eventGateway;
        readonly ParticipantGateway _participantGateway;

        public EventServices(EventGateway eventGateway, ParticipantGateway participantGateway)
        {
            _eventGateway = eventGateway;
            _participantGateway = participantGateway;
        }

        public Result<IEnumerable<Event>> GetAllByUserId(int userId)
        {
            IEnumerable<Event> listEventCreator = _eventGateway.GetAllByUserId(userId);
            IEnumerable<Event> listEventInvited = GetAllEvents( _participantGateway.FindParticipantsOfUser(userId) );
            
            return Result.Success(Status.Ok, listEventCreator.Concat(listEventInvited));
        }

        public Result<Event> GetByIds(int userId, int eventId)
        {
            Event events;
            if ((events = _eventGateway.FindByIds(userId, eventId)) == null) return Result.Failure<Event>(Status.NotFound, "Event not found.");
            return Result.Success(Status.Ok, events);
        }

        public Result<Event> GetById(int id)
        {
            Event events;
            if((events = _eventGateway.FindById(id))== null) return Result.Failure<Event>(Status.NotFound, "Event not found.");
            return Result.Success(Status.Ok, events);
        }

        public Result<int> Delete(int eventId)
        {
            if (_eventGateway.FindById(eventId) == null) return Result.Failure<int>(Status.NotFound, "Event not found.");
            _eventGateway.Delete(eventId);
            return Result.Success(Status.Ok, eventId);
        }

        public Result<Event> UpdateEvent(int eventId, int userId, string eventName, string descriptions, DateTime dates)
        {
            if (!IsNameValid(eventName)) return Result.Failure<Event>(Status.BadRequest, "The event's name is not valid.");
            Event events;
            if ((events = _eventGateway.FindById(eventId)) == null)
            {
                return Result.Failure<Event>(Status.NotFound, "Event not found.");
            }

            {
                Event p = _eventGateway.FindByName(eventName);
                if (p != null && p.EventId == eventId) return Result.Failure<Event>(Status.BadRequest, "A Event with this name already exists.");
            }
            _eventGateway.Update(eventId, eventName, descriptions,dates);
            events = _eventGateway.FindById(eventId);

            return Result.Success(Status.Ok, events);
        }
        public Result<int> CreateEvent(int userId, string eventName, string descriptions, DateTime dates)
        {
            if (!IsNameValid(eventName)) return Result.Failure<int>(Status.BadRequest, "The event's name is not valid.");

            int result = _eventGateway.Create(eventName, descriptions, dates, userId);
            
            return Result.Success(Status.Ok, result);
        }

        bool IsNameValid(string name) => !string.IsNullOrWhiteSpace(name);

        IEnumerable<Event> GetAllEvents(IEnumerable<Participant> listParticipant)
        {
            List<Event> listEvent = new List<Event>();
            foreach(Participant participant in listParticipant)
            {
                listEvent.Add(_eventGateway.FindById(participant.EventId));
            }
            return listEvent;
        }
    }
}
