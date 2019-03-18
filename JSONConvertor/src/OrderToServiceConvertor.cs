using Models;
using Newtonsoft.Json.Linq;
using System;

namespace JSONConvertor
{
    public class OrderToServiceConvertor : JsonConvertor<OrderToService>
    {
        public override OrderToService fromJson(JObject orderToServiceJson)
        {
            if (orderToServiceJson == null)
            {
                Logger.Error("Cannot convert order to service from json, value is null");
				return null;
			}

            OrderToService orderToService = new OrderToService()
            {
                Id = Convert.ToInt32(orderToServiceJson["Id"]),
                IdOrder = Convert.ToInt32(orderToServiceJson["IdOrder"]),
                IdService = Convert.ToInt32(orderToServiceJson["IdService"])
            };

            return orderToService;
        }

        public override JObject toJson(OrderToService orderToService)
        {
            if (orderToService == null)
            {
                Logger.Error("Cannot convert order to service to json, value is null");
				return null;
			}

            JObject orderToServiceJson = new JObject();
            orderToServiceJson["Id"] = orderToService.Id;
            orderToServiceJson["IdOrder"] = orderToService.IdOrder;
            orderToServiceJson["IdService"] = orderToService.IdService;

            return orderToServiceJson;
        }
    }
}
