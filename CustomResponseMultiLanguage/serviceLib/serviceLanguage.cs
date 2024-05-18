using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomResponseMultiLanguage.serviceLib
{
    public class serviceLanguage
    {
        HttpContext context = HttpContext.Current;

        public Dictionary<string,string> LoadLngDict(string lang)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            try
            {
                string filePath = context.Server.MapPath("~/Resource/dictionary.json");
                if (System.IO.File.Exists(filePath))
                {
                    string jsonData = System.IO.File.ReadAllText(filePath);
                    JArray array = JArray.Parse(jsonData);
                    foreach (JToken item in array)
                    {
                        string Wording = item["wording"].ToString();
                        string Value = item[lang].ToString();

                        result.Add(Wording, Value);
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }

            return result;
        }
    }
}