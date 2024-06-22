using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Prism.Mvvm;

namespace SdHelper.Models
{
    public class TextWrapper : BindableBase
    {
        private string text = string.Empty;
        private bool isTextChanged;
        private ObservableCollection<Word> words = new ();

        public string Text
        {
            get => text;
            set
            {
                if (SetProperty(ref text, value))
                {
                    IsTextChanged = true;
                }
            }
        }

        public bool IsTextChanged { get => isTextChanged; set => SetProperty(ref isTextChanged, value); }

        public ObservableCollection<Word> Words
        {
            get
            {
                if (IsTextChanged)
                {
                    var formatted = Regex.Replace(Text, " *\n *", ",\n,");
                    formatted = Regex.Replace(formatted, ",,\n", ",\n");

                    words = new ObservableCollection<Word>(formatted.Split(',')
                        .Select(s => new Word() { Text = s.Trim(' '), })
                        .Where(w => !w.IsEmpty));

                    var inParentheses = false;
                    foreach (var w in words)
                    {
                        w.IsOpeningBrackets = w.Text.StartsWith("(");
                        w.IsClosingBrackets = w.Text.EndsWith(")");

                        if (w.IsOpeningBrackets)
                        {
                            inParentheses = true;
                            const string pattern = "^\\(+";
                            w.BracketsCount = Regex.Match(w.Text, pattern).Length; // 開括弧のカウント
                        }

                        w.IsInParentheses = inParentheses;

                        if (w.IsClosingBrackets)
                        {
                            inParentheses = false;
                            const string pattern = "\\)+$";
                            w.BracketsCount = Regex.Match(w.Text, pattern).Length; // 閉じ括弧のカウント
                        }
                    }
                }

                return words;
            }
            private set => SetProperty(ref words, value);
        }
    }
}