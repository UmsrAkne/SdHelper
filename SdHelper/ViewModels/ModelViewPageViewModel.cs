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
    public class ModelViewPageViewModel : BindableBase
    {
        private FileInfoWrapper selectedFileInfo;
        private ImageSource previewImageSource;
        private FileInfo tempPreviewImageFileInfo;
        private bool waitForConfirm;

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
                    PreviewImageSource = File.Exists(exceptImagePath)
                        ? new BitmapImage(new Uri(exceptImagePath))
                        : null;

                    var jsonPath = value.GetFullNameWithoutExtension() + ".json";
                    ModelDetail = File.Exists(jsonPath)
                        ? JsonConvert.DeserializeObject<ModelDetail>(File.ReadAllText(jsonPath))
                        : new ModelDetail();

                    RaisePropertyChanged(nameof(ModelDetail));

                    WaitForConfirm = false;
                    TempPreviewImageFileInfo = null;
                }
            }
        }

        public ImageSource PreviewImageSource
        {
            get => previewImageSource;
            private set
            {
                PreviewImageRect = value == null
                    ? new Rect(0, 0, 0, 0)
                    : new Rect(0, 0, value.Width, value.Height);

                RaisePropertyChanged(nameof(PreviewImageRect));
                SetProperty(ref previewImageSource, value);
            }
        }

        public Rect PreviewImageRect { get; set; }

        public ModelDetail ModelDetail { get; set; } = new ();

        public FileInfo TempPreviewImageFileInfo
        {
            get => tempPreviewImageFileInfo;
            set => SetProperty(ref tempPreviewImageFileInfo, value);
        }

        /// <summary>
        /// プレビュー画像の変更待ちを表すプロパティ
        /// </summary>
        /// <value>
        /// プレビュー画像が変更が未確定の時に true それ以外の時は false
        /// </value>
        public bool WaitForConfirm { get => waitForConfirm; set => SetProperty(ref waitForConfirm, value); }

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

        public DelegateCommand ConfirmPreviewImageChangeCommand => new DelegateCommand(() =>
        {
            if (SelectedFileInfo == null || TempPreviewImageFileInfo == null)
            {
                return;
            }

            var destPath = $"{SelectedFileInfo.GetFullNameWithoutExtension()}.png";
            File.Copy(TempPreviewImageFileInfo.FullName, destPath);
            TempPreviewImageFileInfo = null;
            WaitForConfirm = false;
        });

        public void ReplacePreviewImage(string imageFilePath)
        {
            if (SelectedFileInfo == null)
            {
                return;
            }

            var imageFile = new FileInfo(imageFilePath);
            TempPreviewImageFileInfo = new FileInfo(imageFile.FullName);
            PreviewImageSource = new BitmapImage(new Uri(imageFile.FullName));
            WaitForConfirm = true;
        }
    }
}