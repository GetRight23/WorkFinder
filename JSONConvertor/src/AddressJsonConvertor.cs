using System;
using Models;
using Newtonsoft.Json.Linq;

namespace JSONConvertor
{
    public class AddressJsonConvertor : JsonConvertor<Address>
    {
        public override Address fromJson(JObject addressJson)
        {
            if (addressJson == null)
            {
                Logger.Error("Cannot convert address from json, value is null");
                throw new ArgumentNullException();
            }

            Address address = new Address()
            {
                Id = Convert.ToInt32(addressJson["Id"]),
                IdCity = Convert.ToInt32(addressJson["IdCity"]),
                IdCityDistrict = Convert.ToInt32(addressJson["IdCityDistrict"]),
                ApptNum = Convert.ToString(addressJson["ApptNum"]),
                StreetName = Convert.ToString(addressJson["StreetName"]),
            };

            return address;
        }

        public override JObject toJson(Address address)
        {
            if (address == null)
            {
                Logger.Error("Cannot convert city to json, value is null");
                throw new ArgumentNullException();
            }

            JObject addressJson = new JObject();
            addressJson["Id"] = address.Id;
            addressJson["StreetName"] = address.StreetName;
            addressJson["ApptNum"] = address.ApptNum;
            addressJson["IdCityDistrict"] = address.IdCityDistrict;
            addressJson["IdCity"] = address.IdCity;

            return addressJson;
        }
    }
}
