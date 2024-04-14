using System.IO;
using System.Windows;
using Microsoft.Xaml.Behaviors;
using SdHelper.Models;
using SdHelper.ViewModels;

namespace SdHelper.Views.Behaviors
{
    public class FileDropBehavior : Behavior<UIElement>
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
            var window = sender as Window;
            if (window == null)
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

                switch (fileInfo.Extension)
                {
                    // ReSharper disable once StringLiteralTypo
                    case ".safetensors":
                        ((MainWindowViewModel)window.DataContext).ModelFileInfos.Add(new FileInfoWrapper(new FileInfo(f)));
                        break;
                    case ".png":
                        ((MainWindowViewModel)window.DataContext).ReplacePreviewImage(fileInfo.FullName);
                        return;
                }
            }
        }
    }
}