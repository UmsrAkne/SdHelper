using System.Windows.Media;
using Prism.Mvvm;

namespace SdHelper.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ScaleManagementViewModel : BindableBase
    {
        private double scale = 1.0;
        private bool lockScale;
        private bool isFitDisplay;
        private Stretch stretch;

        public double Scale { get => scale; set => SetProperty(ref scale, value); }

        public bool LockScale { get => lockScale; set => SetProperty(ref lockScale, value); }

        /// <summary>
        /// 画像をコンテナのサイズにフィットさせて表示するかどうかを設定・取得します。
        /// </summary>
        /// <value>
        /// 画像をコンテナのサイズにフィットさせて表示するか。
        /// </value>
        public bool IsFitDisplay
        {
            get => isFitDisplay;
            set
            {
                Stretch = value ? Stretch.Uniform : Stretch.None;
                SetProperty(ref isFitDisplay, value);
            }
        }

        public Stretch Stretch { get => stretch; set => SetProperty(ref stretch, value); }
    }
}