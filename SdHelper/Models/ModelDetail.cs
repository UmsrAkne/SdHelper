using System.Text.Json;

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
        public string SerializeToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}