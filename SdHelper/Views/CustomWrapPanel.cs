using System;
using System.Windows;
using System.Windows.Controls;

namespace SdHelper.Views
{
    public class CustomWrapPanel : WrapPanel
    {
        /// <summary>
        /// 子要素のサイズを測定し、パネル全体のサイズを決定します。要素が行を超える場合や改行の指示がある場合に行のサイズを調整します。
        /// </summary>
        /// <param name="constraint">子要素が入る制約幅。</param>
        /// <returns>最終的なパネルのサイズ。</returns>
        protected override Size MeasureOverride(Size constraint)
        {
            var currentLineSize = new Size(0, 0);
            var panelSize = new Size(0, 0);

            foreach (UIElement element in Children)
            {
                element.Measure(constraint);
                var elementSize = element.DesiredSize;

                var isBreak = element is FrameworkElement { Tag: string and "Break", };

                if (currentLineSize.Width + elementSize.Width > constraint.Width || isBreak)
                {
                    panelSize.Width = Math.Max(currentLineSize.Width, panelSize.Width);
                    panelSize.Height += currentLineSize.Height;

                    currentLineSize = isBreak ? new Size(0, 0) : elementSize;
                }
                else
                {
                    currentLineSize.Width += elementSize.Width;
                    currentLineSize.Height = Math.Max(elementSize.Height, currentLineSize.Height);
                }
            }

            panelSize.Width = Math.Max(currentLineSize.Width, panelSize.Width);
            panelSize.Height += currentLineSize.Height;

            return panelSize;
        }

        /// <summary>
        /// 測定されたサイズに基づいて、子要素をパネル内に実際に配置します。要素が行を超える場合や改行の指示がある場合に行の位置を調整します。
        /// </summary>
        /// <param name="finalSize">子要素が入る制約幅。</param>
        /// <returns>引数として渡された最終的なサイズをそのまま返します。</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            var currentLineSize = new Size(0, 0);
            double currentY = 0;

            foreach (UIElement element in Children)
            {
                var elementSize = element.DesiredSize;
                var isBreak = element is FrameworkElement { Tag: string and "Break", };

                if (currentLineSize.Width + elementSize.Width > finalSize.Width || isBreak)
                {
                    currentY += currentLineSize.Height;
                    currentLineSize = isBreak ? new Size(0, 0) : elementSize;
                }
                else
                {
                    currentLineSize.Width += elementSize.Width;
                    currentLineSize.Height = Math.Max(elementSize.Height, currentLineSize.Height);
                }

                element.Arrange(new Rect(new Point(currentLineSize.Width - elementSize.Width, currentY), elementSize));
            }

            return finalSize;
        }
    }
}