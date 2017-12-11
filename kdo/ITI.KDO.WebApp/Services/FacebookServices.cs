using ITI.KDO.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Services
{
    public class FacebookServices
    {
        readonly FacebookClient _facebookClient;
        readonly UserGateway _userGateway;

        public FacebookServices(FacebookClient facebookClient, UserGateway userGateway)
        {
            _facebookClient = facebookClient;
            _userGateway = userGateway;
        }

        public async Task<Result<IEnumerable<string>>> GetFacebookContacts(int userId)
        {
            User user = _userGateway.FindById(userId);
            if (user == null) return Result.Failure<IEnumerable<string>>(Status.NotFound, "User not found.");
            if (user.FacebookAccessToken == string.Empty) return Result.Failure<IEnumerable<string>>(Status.BadRequest, "This user has not facebook account.");

            IEnumerable<string> friends = await _facebookClient.GetFacebookFriends(user.FacebookAccessToken);

            return Result.Success(Status.Ok, friends);
        }

    }
}
