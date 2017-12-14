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

        public ParticipantServices(ParticipantGateway participantGateway)
        {
            _participantGateway = participantGateway;
        }
        public Result <IEnumerable<Participant>>FindById(int userId,int eventId)
        {
            return Result.Success(Status.Ok, _participantGateway.FindParticipantsForEvent(eventId));
        }
    }
}
