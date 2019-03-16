using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WorkFinderAPI
{
	public class JsonHandler
	{
		private JObject m_JObject;
		private JArray m_errorArray = null;

		public JsonHandler()
		{
			m_JObject = new JObject();
			m_errorArray = new JArray();
		}

		public string getJson(JObject jObject)
		{
			m_JObject["Value"] = jObject;
			m_JObject["Errors"] = m_errorArray;
			return m_JObject.ToString();
		}

		public string getJson()
		{
			m_JObject["Value"] = new JObject();
			m_JObject["Errors"] = m_errorArray;
			return m_JObject.ToString();
		}

		public void appendError(string error)
		{
			JObject errorObject = new JObject();
			errorObject["ErrorMessage"] = error;
			m_errorArray.Add(errorObject);
		}
	}
}
