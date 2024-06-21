using System;
using System.Windows;
using System.Windows.Controls;
using SdHelper.Models;

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
            var lineMaxWidth = double.IsInfinity(constraint.Width) ? double.MaxValue : constraint.Width;

            foreach (UIElement element in Children)
            {
                element.Measure(constraint);
                var elementSize = element.DesiredSize;

                var isBreak = element is FrameworkElement { DataContext: Word { IsNewLine: true, }, };

                if (currentLineSize.Width + elementSize.Width > constraint.Width || isBreak)
                {
                    panelSize.Width = Math.Max(currentLineSize.Width, panelSize.Width);
                    panelSize.Height += currentLineSize.Height;

                    currentLineSize = isBreak ? new Size(0, 0) : elementSize;

                    if (isBreak)
                    {
                        currentLineSize.Width = lineMaxWidth; // 改行要素だった場合は、描画する行を維持する。
                    }
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
            var cy = currentLineSize.Height;

            foreach (UIElement element in Children)
            {
                var elementSize = element.DesiredSize;
                var isBreak = element is FrameworkElement { DataContext: Word { IsNewLine: true, }, };

                if (currentLineSize.Width + elementSize.Width > finalSize.Width || isBreak)
                {
                    if (isBreak)
                    {
                        currentY += currentLineSize.Height;
                        cy = currentLineSize.Height;

                        currentLineSize.Width += elementSize.Width;
                        currentLineSize.Height = Math.Max(elementSize.Height, currentLineSize.Height);

                        currentY -= cy;
                    }
                    else if (currentLineSize.Width + elementSize.Width > finalSize.Width)
                    {
                        currentY += currentLineSize.Height;
                        cy = currentLineSize.Height;
                    }
                }
                else
                {
                    currentLineSize.Width += elementSize.Width;
                    currentLineSize.Height = Math.Max(elementSize.Height, currentLineSize.Height);
                }

                element.Arrange(new Rect(new Point(currentLineSize.Width - elementSize.Width, currentY), elementSize));

                if (!isBreak)
                {
                    continue;
                }

                // 要素が改行の指示だった場合に実行される。
                // 行末に要素を表示するのに、currentY を維持していたため、 描画位置を cy の値だけ下へずらす。
                currentY += cy;

                // 次の要素からは、描画は新しい行の左端から開始するので、行の幅を 0 に設定。
                currentLineSize.Width = 0;
            }

            return finalSize;
        }
    }
}