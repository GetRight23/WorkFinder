using Models;
using Newtonsoft.Json.Linq;
using System;

namespace JSONConvertor
{
    public class CityDistrictJsonConvertor : JsonConvertor<CityDistricts>
    {
        public override CityDistricts fromJson(JObject cityDistrictJson)
        {
            if (cityDistrictJson == null)
            {
                Logger.Error("Cannot convert city district from json, value is null");
                throw new ArgumentNullException();
            }

            CityDistricts cityDistrict = new CityDistricts()
            {
                Id = Convert.ToInt32(cityDistrictJson["Id"]),
                Name = Convert.ToString(cityDistrictJson["Name"]),
                IdCity = Convert.ToInt32(cityDistrictJson["IdCity"])
            };

            return cityDistrict;
        }

        public override JObject toJson(CityDistricts cityDistrict)
        {
            if (cityDistrict == null)
            {
                Logger.Error("Cannot convert city district to json, value is null");
                throw new ArgumentNullException();
            }

            JObject cityDistrictJson = new JObject();
            cityDistrictJson["Id"] = cityDistrict.Id;
            cityDistrictJson["Name"] = cityDistrict.Name;
            cityDistrictJson["IdCity"] = cityDistrict.IdCity;

            return cityDistrictJson;
        }
    }
}
