using System.Collections.ObjectModel;
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
    }
}