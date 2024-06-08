using System.Collections.ObjectModel;

namespace SdHelper.Models
{
    public class ImageFileProvider : IImageFileProvider
    {
        public ObservableCollection<FileInfoWrapper> ImageFiles { get; set; }
    }
}