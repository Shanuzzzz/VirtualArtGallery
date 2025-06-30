using System.Collections.Generic;
using System.IO;

namespace VirtualArtGallery.util
{
    public class PropertyUtil
    {
        public static Dictionary<string, string> LoadProperties(string filePath)
        {
            var properties = new Dictionary<string, string>();
            foreach (var line in File.ReadAllLines(filePath))
            {
                if (!string.IsNullOrWhiteSpace(line) && !line.Trim().StartsWith("#"))
                {
                    var parts = line.Split(new[] { '=' }, 2); 
                    if (parts.Length == 2)
                    {
                        properties[parts[0].Trim()] = parts[1].Trim();
                    }
                }
            }
            return properties;
        }
    }
}
