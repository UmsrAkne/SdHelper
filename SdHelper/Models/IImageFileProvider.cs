using System.Collections.ObjectModel;

namespace SdHelper.Models
{
    public interface IImageFileProvider
    {
        public ObservableCollection<FileInfoWrapper> ImageFiles { get; set; }
    }
}