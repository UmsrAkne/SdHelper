using Prism.Mvvm;

namespace SdHelper.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ScaleManagementViewModel : BindableBase
    {
        private double scale = 1.0;
        private bool lockScale;

        public double Scale { get => scale; set => SetProperty(ref scale, value); }

        public bool LockScale { get => lockScale; set => SetProperty(ref lockScale, value); }
    }
}