using Prism.Mvvm;

namespace SdHelper.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";

        public MainWindowViewModel(ModelViewPageViewModel modelViewPageVm)
        {
            ModelViewPageViewModel = modelViewPageVm;
        }

        public ModelViewPageViewModel ModelViewPageViewModel { get; set; }

        public string Title { get => title; set => SetProperty(ref title, value); }
    }
}