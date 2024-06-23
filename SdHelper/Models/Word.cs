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

        public bool IsEmpty => !IsNewLine && Text == string.Empty;

        /// <summary>
        /// このテキストが開き括弧で始まっているかを表します。
        /// </summary>
        public bool IsOpeningBrackets { get; set; }

        /// <summary>
        /// このテキストが閉じ括弧で終わっているかを表します。
        /// </summary>
        public bool IsClosingBrackets { get; set; }

        /// <summary>
        /// このテキストが丸括弧の中に配置されているかを表します。
        /// </summary>
        public bool IsInParentheses { get; set; }

        /// <summary>
        /// このテキストについている括弧の数を表します
        /// </summary>
        public int BracketsCount { get; set; }

        /// <summary>
        /// このテキストの適用の強さを表します。
        /// </summary>
        public decimal Strength { get; set; } = 1.0m;
    }
}