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
				return null;
			}
			JObject jsonObject = new JObject();
			jsonObject["Id"] = address.Id;
			jsonObject["StreetName"] = address.StreetName;
			jsonObject["ApptNum"] = address.ApptNum;
			jsonObject["IdCityDistrict"] = address.IdCityDistrict;
			jsonObject["IdCity"] = address.IdCity;
			return jsonObject;
		}
		public JObject toJson(City city)
		{
			if (city == null)
			{
				return null;
			}
			JObject jsonObject = new JObject();
			jsonObject["Id"] = city.Id;
			jsonObject["Name"] = city.Name;
			return jsonObject;
		}
		public JObject toJson(CityDistricts cityDistricts)
		{
			if (cityDistricts == null)
			{
				return null;
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
				return null;
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
		public JObject toJson(OrdersList orderslist)
		{
			if (orderslist == null)
			{
				return null;
			}
			JObject jsonObject = new JObject();
			jsonObject["Id"] = orderslist.Id;
			jsonObject["IdWorker"] = orderslist.IdWorker;
			return jsonObject;
		}
		public JObject toJson(Order orderTable)
		{
			if (orderTable == null)
			{
				return null;
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
				return null;
			}
			JObject jsonObject = new JObject();
			jsonObject["Id"] = orderToService.Id;
			jsonObject["IdOrder"] = orderToService.IdOrder;
			jsonObject["IdService"] = orderToService.IdService;
			return jsonObject;
		}
		public JObject toJson(ProfessionCategory profCategory)
		{
			if (profCategory == null)
			{
				return null;
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
				return null;
			}
			JObject jsonObject = new JObject();
			jsonObject["Id"] = profession.Id;
			jsonObject["Name"] = profession.Name;
			jsonObject["IdProfCategory"] = profession.IdProfCategory;
			return jsonObject;
		}
		public JObject toJson(Service service)
		{
			if (service == null)
			{
				return null;
			}
			JObject jsonObject = new JObject();
			jsonObject["Id"] = service.Id;
			jsonObject["Price"] = service.Price;
			jsonObject["Name"] = service.Name;
			jsonObject["IdProfession"] = service.IdProfession;
			return jsonObject;
		}
		public JObject toJson(Worker worker)
		{
			if (worker == null)
			{
				return null;
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

		public JObject toJson(User user)
		{
			if (user == null)
			{
				return null;
			}
			JObject jsonObject = new JObject();
			jsonObject["Id"] = user.Id;
			jsonObject["Login"] = user.Login;
			jsonObject["Password"] = user.Password;
			jsonObject["IdWorker"] = user.IdWorker;
			return jsonObject;
		}

		public JObject toJson(Photo photo)
		{
			if (photo == null)
			{
				return null;
			}
			JObject jsonObject = new JObject();
			jsonObject["Id"] = photo.Id;
			jsonObject["Link"] = photo.Link;
			jsonObject["IdUser"] = photo.IdUser;
			return jsonObject;
		}

		public Worker fromJsonToWorker(JObject jsonObject)
		{
			if (jsonObject == null)
			{
				return null;
			}
			Worker worker = new Worker()
			{
				Id = Convert.ToInt32(jsonObject["Id"]),
				PhoneNumber = Convert.ToString(jsonObject["PhoneNumber"]),
				Info = Convert.ToString(jsonObject["Info"]),
				IdAddress = Convert.ToInt32(jsonObject["IdAddress"]),
				Name = Convert.ToString(jsonObject["Name"]),
				LastName = Convert.ToString(jsonObject["LastName"])
			};
			return worker;
		}

		public Service fromJsonToService(JObject jsonObject)
		{
			if (jsonObject == null)
			{
				return null;
			}
			Service service = new Service()
			{
				Id = Convert.ToInt32(jsonObject["Id"]),
				Price = Convert.ToDecimal(jsonObject["Price"]),
				Name = Convert.ToString(jsonObject["Name"]),
				IdProfession = Convert.ToInt32(jsonObject["IdProfession"])
			};
			return service;
		}

		public Profession fronJsonToProfession(JObject jsonObject)
		{
			if (jsonObject == null)
			{
				return null;
			}
			Profession profession = new Profession()
			{
				Id = Convert.ToInt32(jsonObject["Id"]),
				Name = Convert.ToString(jsonObject["Name"]),
				IdProfCategory = Convert.ToInt32(jsonObject["IdProfCategory"])
			};
			return profession;
		}

		public ProfessionCategory fromJsonToProfCategory(JObject jsonObject)
		{
			if (jsonObject == null)
			{
				return null;
			}
			ProfessionCategory profCategory = new ProfessionCategory()
			{
				Id = Convert.ToInt32(jsonObject["Id"]),
				Name = Convert.ToString(jsonObject["Name"])
			};
			return profCategory;
		}

		public OrderToService fromJsonToOrderToService(JObject jsonObject)
		{
			if (jsonObject == null)
			{
				return null;
			}
			OrderToService orderToService = new OrderToService()
			{
				Id = Convert.ToInt32(jsonObject["Id"]),
				IdOrder = Convert.ToInt32(jsonObject["IdOrder"]),
				IdService = Convert.ToInt32(jsonObject["IdService"])
			};
			return orderToService;
		}

		public Order fromJsonToOrderTable(JObject jsonObject)
		{
			if (jsonObject == null)
			{
				return null;
			}
			Order orderTable = new Order()
			{
				Id = Convert.ToInt32(jsonObject["Id"]),
				Info = Convert.ToString(jsonObject["Info"]),
				IdOrderList = Convert.ToInt32(jsonObject["IdOrderList"])
			};
			return orderTable;
		}

		public OrdersList fromJsonToOrderslist(JObject jsonObject)
		{
			if (jsonObject == null)
			{
				return null;
			}
			OrdersList orderslist = new OrdersList()
			{
				Id = Convert.ToInt32(jsonObject["Id"]),
				IdWorker = Convert.ToInt32(jsonObject["IdWorker"])
			};
			return orderslist;
		}

		public Feedback fromJsonToFeedback(JObject jsonObject)
		{
			if (jsonObject == null)
			{
				return null;
			}
			Feedback feedback = new Feedback()
			{
				Id = Convert.ToInt32(jsonObject["Id"]),
				Name = Convert.ToString(jsonObject["Name"]),
				Patronymic = Convert.ToString(jsonObject["Patronymic"]),
				GradeValue = Convert.ToInt32(jsonObject["GradeValue"]),
				Date = Convert.ToDateTime(jsonObject["Date"]),
				Text = Convert.ToString(jsonObject["Text"]),
				IdWorker = Convert.ToInt32(jsonObject["IdWorker"])
			};
			return feedback;
		}

		public CityDistricts fromJsonToCityDistricts(JObject jsonObject)
		{
			if (jsonObject == null)
			{
				return null;
			}
			CityDistricts cityDistricts = new CityDistricts()
			{
				Id = Convert.ToInt32(jsonObject["Id"]),
				Name = Convert.ToString(jsonObject["Name"]),
				IdCity = Convert.ToInt32(jsonObject["IdCity"])
			};
			return cityDistricts;
		}

		public City fromJsonToCity(JObject jsonObject)
		{
			if (jsonObject == null)
			{
				return null;
			}
			City city = new City()
			{
				Id = Convert.ToInt32(jsonObject["Id"]),
				Name = Convert.ToString(jsonObject["Name"])
			};
			return city;
		}

		public Address fromJsonToAddress(JObject jsonObject)
		{
			if (jsonObject == null)
			{
				return null;
			}
			Address address = new Address()
			{
				Id = Convert.ToInt32(jsonObject["Id"]),
				IdCity = Convert.ToInt32(jsonObject["IdCity"]),
				IdCityDistrict = Convert.ToInt32(jsonObject["IdCityDistrict"]),
				ApptNum = Convert.ToString(jsonObject["ApptNum"]),
				StreetName = Convert.ToString(jsonObject["StreetName"]),
			};
			return address;
		}

		public User fromJsonToUser(JObject jsonObject)
		{
			if (jsonObject == null)
			{
				return null;
			}
			User user = new User()
			{
				Id = Convert.ToInt32(jsonObject["Id"]),
				IdWorker = Convert.ToInt32(jsonObject["IdWorker"]),
				Login = Convert.ToString(jsonObject["Login"]),
				Password = Convert.ToString(jsonObject["Password"])
			};
			return user;
		}

		public Photo fromJsonToPhoto(JObject jsonObject)
		{
			if (jsonObject == null)
			{
				return null;
			}
			Photo photo = new Photo()
			{
				Id = Convert.ToInt32(jsonObject["Id"]),
				IdUser = Convert.ToInt32(jsonObject["IdUser"]),
				Link = Convert.ToString(jsonObject["Link"])
			};
			return photo;
		}
	}
}
