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

        public ObservableCollection<FileInfoWrapper> ImageFiles { get; set; } = new ();

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

        public void AddImageFiles(IEnumerable<FileInfoWrapper> images)
        {
            images = images.Except(ImageFiles, new FileInfoWrapperEqualityComparer());

            ImageFiles.AddRange(images);
            ImageFiles = new ObservableCollection<FileInfoWrapper>(ImageFiles.OrderByDescending(f => f.FileInfo.FullName));
        }
    }
}