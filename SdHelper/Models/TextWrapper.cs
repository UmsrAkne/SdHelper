using System.Collections.ObjectModel;
using System.Linq;
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
                    words = new ObservableCollection<Word>(Text.Split(',').Select(s => new Word() { Text = s, }));
                }

                return words;
            }
            private set => SetProperty(ref words, value);
        }
    }
}