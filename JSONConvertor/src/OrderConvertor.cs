using Models;
using Newtonsoft.Json.Linq;
using System;

namespace JSONConvertor
{
    public class OrderConvertor : JsonConvertor<Order>
    {
        public override Order fromJson(JObject orderJson)
        {
            if (orderJson == null)
            {
                Logger.Error("Cannot convert order from json, value is null");
				return null;
			}

            Order order = new Order()
            {
                Id = Convert.ToInt32(orderJson["Id"]),
                Info = Convert.ToString(orderJson["Info"]),
                IdOrderList = Convert.ToInt32(orderJson["IdOrderList"])
            };

            return order;
        }

        public override JObject toJson(Order order)
        {
            if (order == null)
            {
                Logger.Error("Cannot convert order to json, value is null");
				return null;
			}

            JObject orderJson = new JObject();
            orderJson["Id"] = order.Id;
            orderJson["Info"] = order.Info;
            orderJson["IdOrderList"] = order.IdOrderList;

            return orderJson;
        }
    }
}
