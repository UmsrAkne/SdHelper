namespace SdHelper.Models
{
    /// <summary>
    /// Prompt を構成する一つ一つの単語（熟語）を表します。
    /// Prompt を単語ごとに操作するための機能を実装します。
    /// </summary>
    public class Word
    {
        public string Text { get; set; } = string.Empty;

        public bool IsNewLine => Text is "\n" or "\r\n";
    }
}