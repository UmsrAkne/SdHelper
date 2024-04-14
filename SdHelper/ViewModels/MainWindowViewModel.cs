using System.Collections.ObjectModel;
using System.IO;
using Prism.Commands;
using Prism.Mvvm;
using SdHelper.Models;

namespace SdHelper.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";
        private FileInfo selectedFileInfo;

        public string Title { get => title; set => SetProperty(ref title, value); }

        public ObservableCollection<FileInfo> ModelFileInfos { get; set; } = new ();

        public FileInfo SelectedFileInfo
        {
            get => selectedFileInfo;
            set
            {
                if (SetProperty(ref selectedFileInfo, value))
                {
                    RaisePropertyChanged(nameof(JsonOutputCommand));
                }
            }
        }

        public DelegateCommand JsonOutputCommand => new DelegateCommand(() =>
        {
            if (SelectedFileInfo == null)
            {
                return;
            }

            var weName = Path.GetFileNameWithoutExtension(SelectedFileInfo.FullName);
            var directoryPath = SelectedFileInfo.DirectoryName;
            ModelDetail.SerializeToJson($"{directoryPath}\\{weName}.json");
        });

        private ModelDetail ModelDetail { get; set; } = new ();
    }
}