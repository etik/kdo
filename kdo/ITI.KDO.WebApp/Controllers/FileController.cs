﻿using ITI.KDO.DAL;
using ITI.KDO.WebApp.Authentification;
using ITI.KDO.WebApp.Models.UserViewModels;
using ITI.KDO.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme)]
    public class FileController : Controller
    {
        readonly FileServices _fileServices;
  
        public FileController(FileServices fileServices)
        {
            _fileServices = fileServices;
        }

        /// <summary>
        ///  Update Image User by ID (Controller)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost("img/{id}")]
        public IActionResult UpdateImageUser(int id, List<IFormFile> files, int typeOfPicture)
        {
            Result result = _fileServices.UpdatePicture(id, files, (EType)typeOfPicture);
            return this.CreateResult(result);
        }

    }
}
