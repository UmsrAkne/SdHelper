using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using SdHelper.Models;

namespace SdHelper.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ImageViewGridViewModel : BindableBase
    {
        private FileInfoWrapper selectedImageFile;
        private int selectedImageIndex;

        /// <summary>
        /// デザイン画面でのプレビュー生成時に呼び出されるコンストラクタ。
        /// ダミーのデータをビューモデルにセットします。
        /// 実際のアプリの実行時には呼び出されません。
        /// </summary>
        public ImageViewGridViewModel()
            : this(new DummyImageProvider())
        {
        }

        /// <summary>
        /// DI コンテナから IImageFileProvider を受け取るコンストラクタ。
        /// </summary>
        /// <param name="imageProvider">イメージファイルのリストを提供するインスタンス</param>
        // ReSharper disable once MemberCanBePrivate.Global
        // DI コンテナから呼び出されるため、 private にはできない。
        public ImageViewGridViewModel(IImageFileProvider imageProvider)
        {
            ImageFiles = imageProvider.ImageFiles;
            if (ImageFiles.Count > 0)
            {
                SelectedImageFile = ImageFiles[0];
            }
        }

        public ObservableCollection<FileInfoWrapper> ImageFiles { get; private set; }

        public ScaleManagementViewModel ScaleManagementViewModel { get; init; } = new ();

        public FileInfoWrapper SelectedImageFile
        {
            get => selectedImageFile;
            set => SetProperty(ref selectedImageFile, value);
        }

        public DelegateCommand LoadExistingImagePathsCommand => new DelegateCommand(() =>
        {
            var jsonFileName = "imagePaths.json";
            ImageFiles.AddRange(
                File.Exists(jsonFileName)
                ? JsonConvert.DeserializeObject<IEnumerable<string>>(File.ReadAllText(jsonFileName))
                    .Select(p => new FileInfoWrapper(new FileInfo(p)))
                    .ToList()
                : new List<FileInfoWrapper>());
        });

        public DelegateCommand<object> RatingCommand => new DelegateCommand<object>((param) =>
        {
            if (SelectedImageFile == null)
            {
                return;
            }

            if (int.TryParse(param.ToString(), out var n))
            {
                if (Enum.IsDefined(typeof(Rate), n))
                {
                    SelectedImageFile.MetaData.Rate = (Rate)n;
                }
            }
        });

        public void AddImageFiles(IEnumerable<FileInfoWrapper> images)
        {
            images = images.Except(ImageFiles, new FileInfoWrapperEqualityComparer());

            ImageFiles.AddRange(images);
            ImageFiles = new ObservableCollection<FileInfoWrapper>(ImageFiles.OrderByDescending(f => f.FileInfo.FullName));
        }
    }
}