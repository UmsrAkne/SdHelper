using System.IO;
using Newtonsoft.Json;

namespace SdHelper.Models
{
    public class ModelDetail
    {
        public string Prompt { get; set; } = string.Empty;

        public string NegativePrompt { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double Weight { get; set; }

        public string Note { get; set; } = string.Empty;

        // JSON文字列にシリアル化するメソッド
        public void SerializeToJson(string filePath)
        {
            var str = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(filePath, str);
        }
    }
}