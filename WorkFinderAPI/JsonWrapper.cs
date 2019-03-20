using Newtonsoft.Json.Linq;

namespace WorkFinderAPI
{
	public class JsonWrapper
	{
		private JObject json;
		private JArray errorArray = null;

		public JsonWrapper()
		{
			json = new JObject();
			errorArray = new JArray();
		}

		public string getJson(JObject jObject)
		{
			json["data"] = jObject;
			json["errors"] = errorArray;
			return json.ToString();
		}

		public string getJson(JArray jArray)
		{
			json["data"] = jArray;
			json["errors"] = errorArray;
			return json.ToString();
		}

		public string getJson()
		{
			json["data"] = new JObject();
			json["errors"] = errorArray;
			return json.ToString();
		}

		public void appendError(string error)
		{
			JObject errorObject = new JObject();
			errorObject["ErrorMessage"] = error;
			errorArray.Add(errorObject);
		}
	}
}
