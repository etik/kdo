using ITI.KDO.DAL;
using ITI.KDO.WebApp.Models.CategoryPresentViewModels;
using ITI.KDO.WebApp.Models.EmailViewModels;
using ITI.KDO.WebApp.Models.NotificationViewModels;
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

        public static NotificationViewModel ToNotificationViewModel(this Notification @this)
        {
            return new NotificationViewModel
            {
                NotificationId = @this.NotificationId,
                RecipientsEmail = @this.RecipientsEmail,
                SenderEmail = @this.SenderEmail,
                Descriptions = @this.Descriptions,
                InviteAccept = @this.InviteAccept
            };
        }

        public static ContactViewModel ToContactViewModel(this Contact @this)
        {
            return new ContactViewModel
            {
                ContactId = @this.ContactId,
                FirstEmail = @this.FirstEmail,
                SecondEmail = @this.SecondEmail
            };
        }
    }
}
