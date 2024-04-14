using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Mvvm;
using SdHelper.Models;

namespace SdHelper.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BindableBase
    {
        private string title = "Prism Application";
        private FileInfoWrapper selectedFileInfo;

        public string Title { get => title; set => SetProperty(ref title, value); }

        public ObservableCollection<FileInfoWrapper> ModelFileInfos { get; set; } = new ();

        public FileInfoWrapper SelectedFileInfo
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

            ModelDetail.SerializeToJson(SelectedFileInfo.GetFullNameWithoutExtension() + ".json");
        });

        private ModelDetail ModelDetail { get; set; } = new ();
    }
}