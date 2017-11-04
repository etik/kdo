﻿using System;

namespace ITI.KDO.WebApp.Models.UserViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public int Phone { get; set; }

        public byte[] Password { get; set; }
    }
}
