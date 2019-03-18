using Models;
using Newtonsoft.Json.Linq;
using System;

namespace JSONConvertor
{
    public class ProfessionToWorkerConvertor : JsonConvertor<ProfessionToWorker>
    {
        public override ProfessionToWorker fromJson(JObject professionToWorkerJson)
        {
            if (professionToWorkerJson == null)
            {
                Logger.Error("Cannot convert profession to worker from json, value is null");
                throw new ArgumentNullException();
            }

            ProfessionToWorker professionToWorker = new ProfessionToWorker()
            {
                Id = Convert.ToInt32(professionToWorkerJson["Id"]),
                IdProfession = Convert.ToInt32(professionToWorkerJson["IdProfession"]),
                IdWorker = Convert.ToInt32(professionToWorkerJson["IdWorker"])
            };

            return professionToWorker;
        }

        public override JObject toJson(ProfessionToWorker professionToWorker)
        {
            if (professionToWorker == null)
            {
                Logger.Error("Cannot convert profession to worker to json, value is null");
                throw new ArgumentNullException();
            }

            JObject professionToWorkerJson = new JObject();
            professionToWorkerJson["Id"] = professionToWorker.Id;
            professionToWorkerJson["IdProfession"] = professionToWorker.IdProfession;
            professionToWorkerJson["IdWorker"] = professionToWorker.IdWorker;

            return professionToWorkerJson;
        }
    }
}
