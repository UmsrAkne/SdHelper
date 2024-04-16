using System.IO;
using Newtonsoft.Json;
using Prism.Mvvm;

namespace SdHelper.Models
{
    public class ModelDetail : BindableBase
    {
        private string prompt = string.Empty;
        private string negativePrompt = string.Empty;
        private string description = string.Empty;
        private double weight;
        private string note = string.Empty;

        public string Prompt { get => prompt; set => SetProperty(ref prompt, value); }

        public string NegativePrompt { get => negativePrompt; set => SetProperty(ref negativePrompt, value); }

        public string Description { get => description; set => SetProperty(ref description, value); }

        public double Weight { get => weight; set => SetProperty(ref weight, value); }

        public string Note { get => note; set => SetProperty(ref note, value); }

        // JSON文字列にシリアル化するメソッド
        public void SerializeToJson(string filePath)
        {
            var str = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(filePath, str);
        }
    }
}