using ITI.KDO.DAL;
using ITI.KDO.WebApp.Models.CategoryPresentViewModels;
using ITI.KDO.WebApp.Models.PresentViewModels;
using ITI.KDO.WebApp.Models.UserViewModels;
using ITI.KDO.WebApp.Models.EventViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.KDO.WebApp.Models.ParticipantViewModels;

namespace ITI.KDO.WebApp.Controllers
{
    public static class ModelExtensions
    {
        public static UserViewModel ToUserViewModel( this User @this )
        {
            return new UserViewModel
            {
                Email = @this.Email,
                FirstName = @this.FirstName,
                LastName = @this.LastName,
                Birthdate = @this.Birthdate,
                Phone = @this.Phone,                
                Photo = @this.Photo
            };
        }

        public static PresentViewModel ToPresentViewModel(this Present @this)
        {
            return new PresentViewModel
            {
                PresentId = @this.PresentId,
                PresentName = @this.PresentName,
                Price = @this.Price,
                LinkPresent = @this.LinkPresent,
                CategoryPresentId = @this.CategoryPresentId,
                CategoryName = @this.CategoryName,
                UserId = @this.UserId
            };
        }

        public static EventViewModel ToEventViewModel(this Event @this)
        {
            return new EventViewModel
            {
                EventId = @this.EventId,
                EventName = @this.EventName,
                Descriptions = @this.Descriptions,
                Dates = @this.Dates,
                UserId = @this.UserId
            };
        }

        public static ParticipantViewModel ToParticipantViewModel(this Participant @this)
        {
            return new ParticipantViewModel
            {
                UserId = @this.UserId,
                EventId = @this.EventId,
                ParticipantType = @this.ParticipantType
            };
        }

        public static CategoryPresentViewModel ToCategoryPresentViewModel(this CategoryPresent @this)
        {
            return new CategoryPresentViewModel
            {
                CategoryPresentId = @this.CategoryPresentId,
                CategoryName = @this.CategoryName,
                Link = @this.Link
            };
        }
    }
}
