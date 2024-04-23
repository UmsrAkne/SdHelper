using System.Collections.ObjectModel;
using Prism.Mvvm;
using SdHelper.Models;

namespace SdHelper.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ImageViewGridViewModel : BindableBase
    {
        public ObservableCollection<FileInfoWrapper> ImageFiles { get; set; } = new ();
    }
}