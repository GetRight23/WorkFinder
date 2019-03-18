using Models;
using Newtonsoft.Json.Linq;
using System;

namespace JSONConvertor
{
    public class ProfessionCategoryConvertor : JsonConvertor<ProfessionCategory>
    {
        public override ProfessionCategory fromJson(JObject professionCategoryJson)
        {
            if (professionCategoryJson == null)
            {
                Logger.Error("Cannot convert profession category from json, value is null");
				return null;
			}

            ProfessionCategory professionCategory = new ProfessionCategory()
            {
                Id = Convert.ToInt32(professionCategoryJson["Id"]),
                Name = Convert.ToString(professionCategoryJson["Name"])
            };

            return professionCategory;
        }

        public override JObject toJson(ProfessionCategory professionCategory)
        {
            if (professionCategory == null)
            {
                Logger.Error("Cannot convert profession category to json, value is null");
				return null;
			}

            JObject professionCategoryJson = new JObject();
            professionCategoryJson["Id"] = professionCategory.Id;
            professionCategoryJson["Name"] = professionCategory.Name;

            return professionCategoryJson;
        }
    }
}
