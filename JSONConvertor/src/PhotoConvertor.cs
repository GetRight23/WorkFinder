using Models;
using Newtonsoft.Json.Linq;
using System;

namespace JSONConvertor
{
    public class PhotoConvertor : JsonConvertor<Photo>
    {
        public override Photo fromJson(JObject photoJson)
        {
            if (photoJson == null)
            {
                Logger.Error("Cannot convert photo from json, value is null");
                throw new ArgumentNullException();
            }

            Photo photo = new Photo()
            {
                Id = Convert.ToInt32(photoJson["Id"]),
                IdUser = Convert.ToInt32(photoJson["IdUser"]),
                Link = Convert.ToString(photoJson["Link"])
            };

            return photo;
        }

        public override JObject toJson(Photo photo)
        {
            if (photo == null)
            {
                Logger.Error("Cannot convert photo to json, value is null");
                throw new ArgumentNullException();
            }

            JObject photoJson = new JObject();
            photoJson["Id"] = photo.Id;
            photoJson["IdUser"] = photo.IdUser;
            photoJson["Link"] = photo.Link;

            return photoJson;
        }
    }
}
