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
        private FileInfoWrapper selectedFileInfo;
        private string previewImagePath;

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

                    var exceptImagePath = value.GetFullNameWithoutExtension() + ".png";
                    if (File.Exists(exceptImagePath))
                    {
                        PreviewImagePath = exceptImagePath;
                    }
                }
            }
        }

        public string PreviewImagePath
        {
            get => previewImagePath;
            private set => SetProperty(ref previewImagePath, value);
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