using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;

namespace SdHelper.Models
{
    public class FileInfoWrapper
    {
        public FileInfoWrapper(FileInfo f)
        {
            FileInfo = f;
            if (f.Extension.Equals(".png", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    var bitmap = new BitmapImage(new Uri(f.FullName));
                    Width = bitmap.PixelWidth;
                    Height = bitmap.PixelHeight;

                    var frame = BitmapFrame.Create(new Uri(f.FullName));

                    if (frame.Metadata == null)
                    {
                        return;
                    }

                    MetaData = new ModelDetail();
                    var m = (BitmapMetadata)frame.Metadata;

                    foreach (var query in m)
                    {
                        if (query.StartsWith("/tEXt"))
                        {
                            MetaData.Prompt = m.GetQuery("/tEXt/{str=parameters}") as string;
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    Debug.WriteLine("画像の読み込みに失敗しました");
                    throw;
                }
            }
        }

        public FileInfo FileInfo { get; private set; }

        public string Name => FileInfo.Name;

        public int Width { get; set; }

        public int Height { get; set; }

        public ModelDetail MetaData { get; private set; }

        /// <summary>
        /// このオブジェクトが保持している FileInfo の、拡張子を除いたフルパスを取得します。
        /// </summary>
        /// <returns>拡張子を除いた状態のフルパス</returns>
        public string GetFullNameWithoutExtension()
        {
            return FileInfo.Directory != null
                ? $"{FileInfo.Directory.FullName}\\{Path.GetFileNameWithoutExtension(FileInfo.Name)}"
                : FileInfo.Name;
        }
    }
}