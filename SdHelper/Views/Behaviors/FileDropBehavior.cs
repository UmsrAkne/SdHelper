using System.Windows;
using Microsoft.Xaml.Behaviors;

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
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            }
        }
    }
}