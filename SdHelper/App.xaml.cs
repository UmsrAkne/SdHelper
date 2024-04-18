using System.Windows;
using Prism.Ioc;
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
            containerRegistry.Register<ModelViewPageViewModel>();
        }
    }
}