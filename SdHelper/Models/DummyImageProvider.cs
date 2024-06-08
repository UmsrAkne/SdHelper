using System.Collections.ObjectModel;
using System.IO;

namespace SdHelper.Models
{
    public class DummyImageProvider : IImageFileProvider
    {
        public ObservableCollection<FileInfoWrapper> ImageFiles { get; set; } =
            new ObservableCollection<FileInfoWrapper>()
            {
                new (new FileInfo("test1.png")),
                new (new FileInfo("test2.png")),
                new (new FileInfo("test3.png")),
                new (new FileInfo("test4.png")),
                new (new FileInfo("test5.png")),
                new (new FileInfo("test6.png")),
                new (new FileInfo("test7.png")),
                new (new FileInfo("test8.png")),
            };
    }
}