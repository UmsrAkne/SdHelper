using Prism.Mvvm;

namespace SdHelper.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";

        public MainWindowViewModel(ModelViewGridViewModel modelViewGridVm, ImageViewGridViewModel imageViewGridVm)
        {
            ModelViewGridViewModel = modelViewGridVm;
            ImageViewGridViewModel = imageViewGridVm;
        }

        public ModelViewGridViewModel ModelViewGridViewModel { get; set; }

        public ImageViewGridViewModel ImageViewGridViewModel { get; set; }

        public string Title { get => title; set => SetProperty(ref title, value); }
    }
}