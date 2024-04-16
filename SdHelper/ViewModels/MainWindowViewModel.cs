using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
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

                    var jsonPath = value.GetFullNameWithoutExtension() + ".json";
                    ModelDetail = File.Exists(jsonPath)
                        ? JsonConvert.DeserializeObject<ModelDetail>(File.ReadAllText(jsonPath))
                        : new ModelDetail();

                    RaisePropertyChanged(nameof(ModelDetail));
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

        public ModelDetail ModelDetail { get; set; } = new ();

        public DelegateCommand JsonOutputCommand => new DelegateCommand(() =>
        {
            if (SelectedFileInfo == null)
            {
                return;
            }

            ModelDetail.SerializeToJson(SelectedFileInfo.GetFullNameWithoutExtension() + ".json");
        });

        public DelegateCommand SaveModelUrlListCommand => new DelegateCommand(() =>
        {
            const string jsonFileName = "modelUrls.json";
            var existingList = new List<string>();

            if (File.Exists(jsonFileName))
            {
                var jsonFromFile = File.ReadAllText(jsonFileName);
                existingList = JsonConvert.DeserializeObject<List<string>>(jsonFromFile);
            }
            
            var urls = ModelFileInfos.Select(f => f.FileInfo.FullName);
            urls = urls.Union(existingList);
            var json = JsonConvert.SerializeObject(urls, Formatting.Indented);
            File.WriteAllText(jsonFileName, json);
        });

        public DelegateCommand LoadModelUrlsListCommand => new DelegateCommand(() =>
        {
            const string jsonFileName = "modelUrls.json";
            if (!File.Exists(jsonFileName))
            {
                return;
            }

            var jsonFromFile = File.ReadAllText(jsonFileName);
            ModelFileInfos.AddRange(
                JsonConvert.DeserializeObject<List<string>>(jsonFromFile)
                    .Select(s => new FileInfoWrapper(new FileInfo(s))));
        });

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