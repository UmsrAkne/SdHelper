using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;
using SdHelper.ViewModels;

namespace SdHelper.Views.Behaviors
{
    public class PreviewImageDropBehavior : Behavior<UIElement>
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
            if (sender is not Border border)
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
                if (fileInfo.Extension == ".png")
                {
                    ((MainWindowViewModel)border.DataContext).ReplacePreviewImage(fileInfo.FullName);
                    return;
                }
            }
        }
    }
}