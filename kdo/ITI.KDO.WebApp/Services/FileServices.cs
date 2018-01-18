using ITI.KDO.DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Services
{
    public enum EType
    {
        User = 0,
        Present = 1,
        Event = 2
    }

    public class FileServices
    {
        readonly UserGateway _userGateway;
        readonly PresentGateway _presentGateway;



        public FileServices(UserGateway userGateway, PresentGateway presentGateway)
        {
            _userGateway = userGateway;
            _presentGateway = presentGateway;
        }

        private byte[] BuildByteArray( List<IFormFile> files )
        {
            byte[] coverImageBytes = null;

            if( files != null && files.Count != 0 )
            {
                foreach (var file in files)
                {
                    BinaryReader reader = new BinaryReader(file.OpenReadStream());
                    coverImageBytes = reader.ReadBytes((int)file.Length);
                }
                return coverImageBytes;
            }
            else
            {
                return null;
            }
        }

        public Result UpdatePicture(int id, List<IFormFile> files, EType typeOfPicture)
        {
            
            var picture = BuildByteArray(files);
            return picture == null ? UpdatePicture(id, null, typeOfPicture) : UpdatePicture(id, picture, typeOfPicture);
        }
        public Result UpdatePicture(int id, byte[] coverImageBytes, EType typeOfPicture, bool deletePicture = false)
        {
            User user;
            user = _userGateway.FindById(id);
            switch (typeOfPicture){

                case EType.User:
                    if( deletePicture )
                        coverImageBytes = File.ReadAllBytes(@"..\ITI.KDO.WebApp\App\kdo\src\assets\unknow.jpg");
                    else if (coverImageBytes != null)
                        _userGateway.UserSetPhoto(id, coverImageBytes);

                    return Result.Success(Status.Ok);

                case EType.Present:
                    if (deletePicture )
                        coverImageBytes = File.ReadAllBytes(@"..\ITI.KDO.WebApp\App\kdo\src\assets\present.jpg");
                    else if (coverImageBytes != null)
                        _presentGateway.PresentSetPicture(id, coverImageBytes);

                    // TODO: Remove this useless line
                    //var present = _presentGateway.FindByPresentId(id);
                    return Result.Success(Status.Ok);

                case EType.Event:

                    break;
            }

            return Result.Success(Status.Ok);
        }
    }
}
