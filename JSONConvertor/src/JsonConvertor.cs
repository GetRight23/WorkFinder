using Newtonsoft.Json.Linq;
using NLog;

namespace JSONConvertor
{
	public abstract class JsonConvertor<Type>
	{
        public Logger Logger { get; private set; }

        public JsonConvertor()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }

        public abstract Type fromJson(JObject value);

        public abstract JObject toJson(Type value);
	}
}
