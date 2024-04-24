using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;
using Newtonsoft.Json;
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

            var fileInfos = files
                .Select(f => new FileInfo(f))
                .Where(f => f.Extension == ".png")
                .ToList();

            ((ImageViewGridViewModel)listView.DataContext).AddImageFiles(fileInfos.Select(f => new FileInfoWrapper(f)));

            var jsonFileName = "imagePaths.json";

            // このメソッド内で取得したパスのリスト
            var paths = fileInfos.Select(f => f.FullName);

            // json ファイルに存在する既存のパスリスト。三項演算子でファイルがない場合は空になる
            var existingPaths = File.Exists(jsonFileName)
                ? JsonConvert.DeserializeObject<IEnumerable<string>>(File.ReadAllText(jsonFileName)).ToList()
                : new List<string>();

            paths = paths.Except(existingPaths).Concat(existingPaths); // ２つのリストの重複を削除する

            var jsonString = JsonConvert.SerializeObject(paths, Formatting.Indented);
            File.WriteAllText(jsonFileName, jsonString);
        }
    }
}