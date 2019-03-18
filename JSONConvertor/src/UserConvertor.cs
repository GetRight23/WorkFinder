using Models;
using Newtonsoft.Json.Linq;
using System;

namespace JSONConvertor
{
    public class UserConvertor : JsonConvertor<User>
    {
        public override User fromJson(JObject userJson)
        {
            if (userJson == null)
            {
                Logger.Error("Cannot convert user from json, value is null");
				return null;
			}

            User user = new User()
            {
                Id = Convert.ToInt32(userJson["Id"]),
                Login = Convert.ToString(userJson["Login"]),
                Password = Convert.ToString(userJson["Password"])
            };

            return user;
        }

        public override JObject toJson(User user)
        {
            if (user == null)
            {
                Logger.Error("Cannot convert user to json, value is null");
				return null;
			}

            JObject userJson = new JObject();
            userJson["Id"] = user.Id;
            userJson["Login"] = user.Login;
            userJson["Password"] = user.Password;

            return userJson;
        }
    }
}
