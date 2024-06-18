using Prism.Mvvm;

namespace SdHelper.Models
{
    public class TextWrapper : BindableBase
    {
        private string text = string.Empty;
        private bool isTextChanged;

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
    }
}