using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.UnitTests.Utils
{
    public class JsonFileReader
    {
        private readonly static Assembly CurrentAssembly = Assembly.GetExecutingAssembly();

        public static string Read(string embeddedResourceFullName)
        {
            Stream resource = CurrentAssembly.GetManifestResourceStream($"{CurrentAssembly.GetName().Name}.{embeddedResourceFullName}");
            using var resourceStreamReader = new StreamReader(resource);
            string fileData = resourceStreamReader.ReadToEnd();
            return fileData;
        }

        public static T Read<T>(string embeddedResourceFullName)
        {
            Stream resource = CurrentAssembly.GetManifestResourceStream($"{CurrentAssembly.GetName().Name}.{embeddedResourceFullName}");
            using var resourceStreamReader = new StreamReader(resource);
            string fileData = resourceStreamReader.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(fileData);
        }
    }
}
