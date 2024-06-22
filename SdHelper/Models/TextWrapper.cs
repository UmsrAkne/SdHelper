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
                }

                return words;
            }
            private set => SetProperty(ref words, value);
        }
    }
}