using Models;
using Newtonsoft.Json.Linq;
using System;

namespace JsonConvertor
{
    public class OrdersListConvertor : JsonConvertor<OrdersList>
    {
        public override OrdersList fromJson(JObject ordersListJson)
        {
            if (ordersListJson == null)
            {
                Logger.Error("Cannot convert orders list from json, value is null");
				return null;
			}

            OrdersList orderslist = new OrdersList()
            {
                Id = Convert.ToInt32(ordersListJson["Id"]),
                IdWorker = Convert.ToInt32(ordersListJson["IdWorker"])
            };

            return orderslist;
        }

        public override JObject toJson(OrdersList ordersList)
        {
            if (ordersList == null)
            {
                Logger.Error("Cannot convert orders list to json, value is null");
				return null;
			}

            JObject ordersListJson = new JObject();
            ordersListJson["Id"] = ordersList.Id;
            ordersListJson["IdWorker"] = ordersList.IdWorker;

            return ordersListJson;
        }
    }
}
