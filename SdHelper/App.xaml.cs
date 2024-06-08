using System.Windows;
using Prism.Ioc;
using SdHelper.Models;
using SdHelper.ViewModels;
using SdHelper.Views;

namespace SdHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ModelViewGridViewModel>();
            containerRegistry.Register<ImageViewGridViewModel>();

            #if DEBUG
            containerRegistry.Register<IImageFileProvider, DummyImageProvider>();
            #else
            containerRegistry.Register<IImageFIleProvider, ImageFileProvider>();
            #endif
        }
    }
}