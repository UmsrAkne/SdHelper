using System.Collections.ObjectModel;
using System.IO;
using Prism.Mvvm;

namespace SdHelper.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";

        public string Title { get => title; set => SetProperty(ref title, value); }

        public ObservableCollection<FileInfo> ModelFileInfos { get; set; } = new ();
    }
}