using ITI.KDO.DAL;
using ITI.KDO.WebApp.Models.CategoryPresentViewModels;
using ITI.KDO.WebApp.Models.InviteViewModels;
using ITI.KDO.WebApp.Models.PresentViewModels;
using ITI.KDO.WebApp.Models.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public static CategoryPresentViewModel ToCategoryPresentViewModel(this CategoryPresent @this)
        {
            return new CategoryPresentViewModel
            {
                CategoryPresentId = @this.CategoryPresentId,
                CategoryName = @this.CategoryName,
                Link = @this.Link
            };
        }

        public static InviteViewModel ToInviteViewModel(this InviteViewModel @this)
        {
            return new InviteViewModel
            {
                RecipientsMail = @this.RecipientsMail,
                SenderMail = @this.SenderMail,
                Descriptions = @this.Descriptions
            };
        }
    }
}
