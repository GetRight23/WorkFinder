using Models;
using Newtonsoft.Json.Linq;
using System;

namespace JSONConvertor
{
    public class WorkerConvertor : JsonConvertor<Worker>
    {
        public override Worker fromJson(JObject workerJson)
        {
            if (workerJson == null)
            {
                Logger.Error("Cannot convert worker from json, value is null");
                throw new ArgumentNullException();
            }

            Worker worker = new Worker()
            {
                Id = Convert.ToInt32(workerJson["Id"]),
                PhoneNumber = Convert.ToString(workerJson["PhoneNumber"]),
                Info = Convert.ToString(workerJson["Info"]),
                IdAddress = Convert.ToInt32(workerJson["IdAddress"]),
                Name = Convert.ToString(workerJson["Name"]),
                LastName = Convert.ToString(workerJson["LastName"]),
                IdUser = Convert.ToInt32(workerJson["IdUser"])
            };

            return worker;
        }

        public override JObject toJson(Worker worker)
        {
            if (worker == null)
            {
                Logger.Error("Cannot convert worker to json, value is null");
                throw new ArgumentNullException();
            }

            JObject workerJson = new JObject();
            workerJson["Id"] = worker.Id;
            workerJson["PhoneNumber"] = worker.PhoneNumber;
            workerJson["Info"] = worker.Info;
            workerJson["IdAddress"] = worker.IdAddress;
            workerJson["Name"] = worker.Name;
            workerJson["LastName"] = worker.LastName;
            workerJson["IdUser"] = worker.IdUser;

            return workerJson;
        }
    }
}
