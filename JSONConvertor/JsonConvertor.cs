using Models;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JSONConvertor
{
    public class JsonConvertor
    {
        public JObject toJson(Address address)
        {
            if (address == null)
            {
                return new JObject();
            }
            JObject jsonObject = new JObject();
            jsonObject["Id"] = address.Id;
            jsonObject["StreetName"] = address.StreetName;
            jsonObject["ApptNum"] = address.ApptNum;
            jsonObject["IdCityDistrict"] = address.IdCityDistrict;
            jsonObject["IdCity"] = address.IdCity;
            return jsonObject;
        }
        public JObject toJson(City cities)
        {
            if (cities == null)
            {
                return new JObject();
            }
            JObject jsonObject = new JObject();
            jsonObject["Id"] = cities.Id;
            jsonObject["Name"] = cities.Name;
            return jsonObject;
        }
        public JObject toJson(CityDistricts cityDistricts)
        {
            if (cityDistricts == null)
            {
                return new JObject();
            }
            JObject jsonObject = new JObject();
            jsonObject["Id"] = cityDistricts.Id;
            jsonObject["Name"] = cityDistricts.Name;
            jsonObject["IdCity"] = cityDistricts.IdCity;
            return jsonObject;
        }
        public JObject toJson(Feedback feedback)
        {
            if (feedback == null)
            {
                return new JObject();
            }
            JObject jsonObject = new JObject();
            jsonObject["Id"] = feedback.Id;
            jsonObject["Name"] = feedback.Name;
            jsonObject["Patronymic"] = feedback.Patronymic;
            jsonObject["GradeValue"] = feedback.GradeValue;
            jsonObject["Date"] = feedback.Date;
            jsonObject["Text"] = feedback.Text;
            jsonObject["IdWorker"] = feedback.IdWorker;
            return jsonObject;
        }
        public JObject toJson(Orderslist orderslist)
        {
            if (orderslist == null)
            {
                return new JObject();
            }
            JObject jsonObject = new JObject();
            jsonObject["Id"] = orderslist.Id;
            jsonObject["IdWorker"] = orderslist.IdWorker;
            return jsonObject;
        }
        public JObject toJson(OrderTable orderTable)
        {
            if (orderTable == null)
            {
                return new JObject();
            }
            JObject jsonObject = new JObject();
            jsonObject["Id"] = orderTable.Id;
            jsonObject["Info"] = orderTable.Info;
            jsonObject["IdOrderList"] = orderTable.IdOrderList;
            return jsonObject;
        }
        public JObject toJson(OrderToService orderToService)
        {
            if (orderToService == null)
            {
                return new JObject();
            }
            JObject jsonObject = new JObject();
            jsonObject["Id"] = orderToService.Id;
            jsonObject["IdOrder"] = orderToService.IdOrder;
            jsonObject["IdService"] = orderToService.IdService;
            return jsonObject;
        }
        public JObject toJson(ProfCategory profCategory)
        {
            if (profCategory == null)
            {
                return new JObject();
            }
            JObject jsonObject = new JObject();
            jsonObject["Id"] = profCategory.Id;
            jsonObject["Name"] = profCategory.Name;
            return jsonObject;
        }
        public JObject toJson(Profession profession)
        {
            if (profession == null)
            {
                return new JObject();
            }
            JObject jsonObject = new JObject();
            jsonObject["Id"] = profession.Id;
            jsonObject["Name"] = profession.Name;
            jsonObject["IdWorker"] = profession.IdWorker;
            jsonObject["IdProfCategory"] = profession.IdProfCategory;
            return jsonObject;
        }
        public JObject toJson(Service service)
        {
            if (service == null)
            {
                return new JObject();
            }
            JObject jsonObject = new JObject();
            jsonObject["Id"] = service.Id;
            jsonObject["Price"] = service.Price;
            jsonObject["Name"] = service.Name;
            jsonObject["IdProffesion"] = service.IdProffesion;
            return jsonObject;
        }
        public JObject toJson(Worker worker)
        {
            if (worker == null)
            {
                return new JObject();
            }
            JObject jsonObject = new JObject();
            jsonObject["Id"] = worker.Id;
            jsonObject["PhoneNumber"] = worker.PhoneNumber;
            jsonObject["Info"] = worker.Info;
            jsonObject["IdAddress"] = worker.IdAddress;
            jsonObject["Name"] = worker.Name;
            jsonObject["LastName"] = worker.LastName;
            return jsonObject;
        }
    }
}
