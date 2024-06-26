using System.Text.RegularExpressions;
using Prism.Mvvm;

namespace SdHelper.Models
{
    public class ImageDetail : BindableBase
    {
        private Rate rate = Rate.None;

        /// <summary>
        /// 画像ファイルに埋め込まれたメタデータのテキストから読み取れる情報を各プロパティにセットします
        /// </summary>
        /// <param name="metaDataText">画像ファイルから読み込まれたメタデータのテキスト</param>
        public ImageDetail(string metaDataText)
        {
            metaDataText = metaDataText.Replace("\n", " ");

            var match1 = Regex.Match(metaDataText, "(.+?)Negative prompt: ");
            Prompt = match1.Success
                ? match1.Groups[1].Value.Trim()
                : string.Empty;

            var match2 = Regex.Match(metaDataText, "Negative prompt: (.+?) Steps:");
            NegativePrompt = match2.Success
                ? match2.Groups[1].Value.Trim()
                : string.Empty;

            var match3 = Regex.Match(metaDataText, @"Seed: (\d+),");
            Seed = match3.Success
                ? uint.Parse(match3.Groups[1].Value.Trim())
                : 0;
        }

        public ImageDetail()
        {
        }

        public string Prompt { get; set; }

        public string NegativePrompt { get; set; }

        public uint Seed { get; set; }

        public int Width { get; init; }

        public int Height { get; init; }

        public Rate Rate { get => rate; set => SetProperty(ref rate, value); }
    }
}