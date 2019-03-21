using Models;
using Newtonsoft.Json.Linq;
using System;

namespace JsonConvertor
{
    public class PhotoConvertor : JsonConvertor<Photo>
    {
        public override Photo fromJson(JObject photoJson)
        {
            if (photoJson == null)
            {
                Logger.Error("Cannot convert photo from json, value is null");
				return null;
			}

            Photo photo = new Photo()
            {
                Id = Convert.ToInt32(photoJson["Id"]),
                IdUser = Convert.ToInt32(photoJson["IdUser"]),
                Data = Convert.ToByte(photoJson["Data"])
            };

            return photo;
        }

        public override JObject toJson(Photo photo)
        {
            if (photo == null)
            {
                Logger.Error("Cannot convert photo to json, value is null");
				return null;
			}

            JObject photoJson = new JObject();
            photoJson["Id"] = photo.Id;
            photoJson["IdUser"] = photo.IdUser;
            photoJson["Data"] = photo.Data;

            return photoJson;
        }
    }
}
