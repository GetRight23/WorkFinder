using Models;
using Newtonsoft.Json.Linq;
using System;

namespace JSONConvertor
{
    public class ServiceConvertor : JsonConvertor<Service>
    {
        public override Service fromJson(JObject serviceJson)
        {
            if (serviceJson == null)
            {
                Logger.Error("Cannot convert service from json, value is null");
                throw new ArgumentNullException();
            }

            Service service = new Service()
            {
                Id = Convert.ToInt32(serviceJson["Id"]),
                Price = Convert.ToDecimal(serviceJson["Price"]),
                Name = Convert.ToString(serviceJson["Name"]),
                IdProfession = Convert.ToInt32(serviceJson["IdProfession"])
            };

            return service;
        }

        public override JObject toJson(Service service)
        {
            if (service == null)
            {
                Logger.Error("Cannot convert service to json, value is null");
                throw new ArgumentNullException();
            }

            JObject serviceJson = new JObject();
            serviceJson["Id"] = service.Id;
            serviceJson["Price"] = service.Price;
            serviceJson["Name"] = service.Name;
            serviceJson["IdProfession"] = service.IdProfession;

            return serviceJson;
        }
    }
}
