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
			json["Value"] = jObject;
			json["Errors"] = errorArray;
			return json.ToString();
		}

		public string getJson(JArray jArray)
		{
			json["Value"] = jArray;
			json["Errors"] = errorArray;
			return json.ToString();
		}

		public string getJson()
		{
			json["Value"] = new JObject();
			json["Errors"] = errorArray;
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
