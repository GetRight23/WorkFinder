using Models;
using Newtonsoft.Json.Linq;
using System;

namespace JsonConvertor
{
    public class PhotoConvertor : JsonConvertor<Photo>
    {
        public override Photo fromJson(JObject photoJson)
        {
			throw new NotImplementedException();
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
            photoJson["Data"] =  Convert.ToBase64String(photo.Data);

            return photoJson;
        }
    }
}
