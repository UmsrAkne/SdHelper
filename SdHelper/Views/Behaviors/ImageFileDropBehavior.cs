using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;
using SdHelper.Models;
using SdHelper.ViewModels;

namespace SdHelper.Views.Behaviors
{
    /// <summary>
    /// ImageViewGrid の中で使用する Behavior です。
    /// ドラッグアンドドロップされてきた画像ファイルを受け取り、 ImageViewGridViewModel に渡す機能を持ちます。
    /// </summary>
    public class ImageFileDropBehavior : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AllowDrop = true;
            AssociatedObject.Drop += AssociatedObject_Drop;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Drop -= AssociatedObject_Drop;
            base.OnDetaching();
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            var listView = sender as ListView;
            if (listView == null)
            {
                return;
            }

            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files == null || files.Length == 0)
            {
                return;
            }

            foreach (var f in files)
            {
                var fileInfo = new FileInfo(f);
                if (fileInfo.Extension != ".png")
                {
                    continue;
                }

                ((ImageViewGridViewModel)listView.DataContext).ImageFiles.Add(new FileInfoWrapper(new FileInfo(f)));
            }
        }
    }
}