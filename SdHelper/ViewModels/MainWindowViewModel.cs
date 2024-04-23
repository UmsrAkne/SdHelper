using System.Diagnostics;
using Prism.Mvvm;

namespace SdHelper.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";
        private int tabIndex;

        public MainWindowViewModel(ModelViewGridViewModel modelViewGridVm, ImageViewGridViewModel imageViewGridVm)
        {
            ModelViewGridViewModel = modelViewGridVm;
            ImageViewGridViewModel = imageViewGridVm;

            ChangeSelectedTabIndex(1); // デバッグビルド のときだけ実行されるメソッド
        }

        public ModelViewGridViewModel ModelViewGridViewModel { get; set; }

        public ImageViewGridViewModel ImageViewGridViewModel { get; set; }

        public int TabIndex { get => tabIndex; set => SetProperty(ref tabIndex, value); }

        public string Title { get => title; set => SetProperty(ref title, value); }

        [Conditional("DEBUG")]
        private void ChangeSelectedTabIndex(int index)
        {
            TabIndex = index;
        }
    }
}