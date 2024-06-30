using System.Diagnostics;
using Prism.Mvvm;
using SdHelper.Models;

namespace SdHelper.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private int tabIndex;

        public MainWindowViewModel(ModelViewGridViewModel modelViewGridVm, ImageViewGridViewModel imageViewGridVm, PromptNoteViewModel promptNoteViewModel)
        {
            TitleBarText.Text = "Sd Helper";
            ModelViewGridViewModel = modelViewGridVm;
            ImageViewGridViewModel = imageViewGridVm;
            PromptNoteViewModel = promptNoteViewModel;

            ChangeSelectedTabIndex(1); // デバッグビルド のときだけ実行されるメソッド
            SetVersion();
        }

        public ModelViewGridViewModel ModelViewGridViewModel { get; set; }

        public ImageViewGridViewModel ImageViewGridViewModel { get; set; }

        public PromptNoteViewModel PromptNoteViewModel { get; set; }

        public int TabIndex { get => tabIndex; set => SetProperty(ref tabIndex, value); }

        public TitleBarText TitleBarText { get; init; } = new TitleBarText();

        [Conditional("DEBUG")]
        private void ChangeSelectedTabIndex(int index)
        {
            TabIndex = index;
        }

        [Conditional("RELEASE")]
        private void SetVersion()
        {
            TitleBarText.Version = "version " + "20240630" + "a";
        }
    }
}