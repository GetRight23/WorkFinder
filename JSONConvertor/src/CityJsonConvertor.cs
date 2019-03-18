using Models;
using Newtonsoft.Json.Linq;
using System;

namespace JSONConvertor
{
    public class CityJsonConvertor : JsonConvertor<City>
    {
        public override City fromJson(JObject cityJson)
        {
            if (cityJson == null)
            {
                Logger.Error("Cannot convert city from json, value is null");
				return null;
			}

            City city = new City()
            {
                Id = Convert.ToInt32(cityJson["Id"]),
                Name = Convert.ToString(cityJson["Name"])
            };

            return city;
        }

        public override JObject toJson(City city)
        {
            if (city == null)
            {
                Logger.Error("Cannot convert city to json, value is null");
				return null;
			}

            JObject cityJson = new JObject();
            cityJson["Id"] = city.Id;
            cityJson["Name"] = city.Name;
    
            return cityJson;
        }
    }
}
