using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WindowsService
{
    internal class Configuration
    {
        public static string GetInstanceName(object assemblyPath)
        {
            string serviceName;
            try
            {
                string configFile = $"{assemblyPath}.config";
                var configFileContent = ReadFile(configFile);
                serviceName = ReadAppSetting(configFileContent, "serviceName");
            }
            catch
            {
                serviceName = null;
            }
            if (string.IsNullOrEmpty(serviceName)) serviceName = "Keep IIS Alive";
            return serviceName;
        }

        private static string ReadAppSetting(string configuration, string key)
        {
            string result = null;
            if (!string.IsNullOrWhiteSpace(configuration))
            {
                try
                {
                    var xml = XDocument.Parse(configuration);
                    result = xml.Element("configuration")?.Element("appSettings")?.Elements("add").Where(e => e.Attribute("key")?.Value.Equals(key) == true).FirstOrDefault()?.Attribute("value")?.Value;
                }
                catch { }
            }
            return result;
        }

        private static string ReadFile(string fileName)
        {
            string strContents;
            StreamReader objReader;
            objReader = new StreamReader(fileName);
            strContents = objReader.ReadToEnd();
            objReader.Close();
            return strContents;
        }

    }
}
