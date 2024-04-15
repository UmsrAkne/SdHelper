using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
        private ImageSource previewImageSource;

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
                        PreviewImageSource = new BitmapImage(new Uri(exceptImagePath));
                    }
                }
            }
        }

        public ImageSource PreviewImageSource
        {
            get => previewImageSource;
            private set
            {
                PreviewImageRect = new Rect(0, 0, value.Width, value.Height);
                RaisePropertyChanged(nameof(PreviewImageRect));
                SetProperty(ref previewImageSource, value);
            }
        }

        public Rect PreviewImageRect { get; set; }

        public DelegateCommand JsonOutputCommand => new DelegateCommand(() =>
        {
            if (SelectedFileInfo == null)
            {
                return;
            }

            ModelDetail.SerializeToJson(SelectedFileInfo.GetFullNameWithoutExtension() + ".json");
        });

        private ModelDetail ModelDetail { get; set; } = new ();

        public void ReplacePreviewImage(string imageFilePath)
        {
            if (SelectedFileInfo == null)
            {
                return;
            }

            var imageFile = new FileInfo(imageFilePath);
            var destPath = $"{SelectedFileInfo.GetFullNameWithoutExtension()}.png";
            File.Copy(imageFile.FullName, destPath);
            PreviewImageSource = new BitmapImage(new Uri(destPath));
        }
    }
}