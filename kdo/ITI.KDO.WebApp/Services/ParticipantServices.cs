using System;
using ITI.KDO.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Services
{
    public class ParticipantServices
    {
        readonly ParticipantGateway _participantGateway;
        readonly UserGateway _userGateway;
        readonly EventGateway _eventGateway;

        public ParticipantServices(ParticipantGateway participantGateway, UserGateway userGateway, EventGateway eventGateway)
        {
            _participantGateway = participantGateway;
            _userGateway = userGateway;
            _eventGateway = eventGateway;
        }
        public Result <IEnumerable<Participant>>FindById(int userId,int eventId)
        {
            return Result.Success(Status.Ok, _participantGateway.FindParticipantsForEvent(eventId));
        }

        public Result<Participant> CreateParticipant(int userId, int eventId, bool participantType)
        {
            if (_userGateway.FindById(userId) == null) return Result.Failure<Participant>(Status.NotFound, "User not found");
            if (_eventGateway.FindById(eventId) != null) return Result.Failure<Participant>(Status.BadRequest, "Event existed.");

            _participantGateway.Create(userId, eventId, 0);
            Participant participant = _participantGateway.FindById(userId, eventId);
            return Result.Success(Status.Ok, participant);
        }
    }
}
