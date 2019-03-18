using Models;
using Newtonsoft.Json.Linq;
using System;

namespace JSONConvertor
{
    public class ProfessionConvertor : JsonConvertor<Profession>
    {
        public override Profession fromJson(JObject professionJson)
        {
            if (professionJson == null)
            {
                Logger.Error("Cannot convert profession from json, value is null");
                throw new ArgumentNullException();
            }

            Profession profession = new Profession()
            {
                Id = Convert.ToInt32(professionJson["Id"]),
                Name = Convert.ToString(professionJson["Name"]),
                IdProfCategory = Convert.ToInt32(professionJson["IdProfessionCategory"])
            };

            return profession;
        }

        public override JObject toJson(Profession profession)
        {
            if (profession == null)
            {
                Logger.Error("Cannot convert profession to json, value is null");
                throw new ArgumentNullException();
            }

            JObject professionJson = new JObject();
            professionJson["Id"] = profession.Id;
            professionJson["Name"] = profession.Name;
            professionJson["IdProfessionCategory"] = profession.IdProfCategory;

            return professionJson;
        }
    }
}
