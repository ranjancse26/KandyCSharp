using Newtonsoft.Json;

namespace KandyCSharpConsole
{
    public class JsonObjectDumper
    {
        public string WriteToString(object objectToDump)
        {
            return JsonConvert.SerializeObject(objectToDump, Formatting.Indented);
        }
    }
}