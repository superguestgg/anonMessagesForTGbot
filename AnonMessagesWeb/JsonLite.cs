using System.Text;

namespace AnonMessagesWeb;

public class JsonLite
{
    public static Dictionary<string, string> DecodeDictionary(string s)
    {
        throw new NotImplementedException();
    }
    
    public static string EncodeDictionary(Dictionary<string, string> d)
    {
        var result = new StringBuilder();
        result.Append('{');
        foreach (var kv in d)
        {
            result.Append($"\"{kv.Key}\":\"{kv.Value}\", ");
        }
        if (d.Count > 0) 
            result.Remove(result.Length - 2, 2);
        result.Append('}');
        return result.ToString();
    }
}