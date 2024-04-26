using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace SdHelper.Models
{
    public class FileInfoWrapper
    {
        private ImageDetail metaData;

        public FileInfoWrapper(FileInfo f)
        {
            FileInfo = f;
        }

        public FileInfo FileInfo { get; private set; }

        public string Name => FileInfo.Name;

        public ImageDetail MetaData
        {
            get
            {
                if (metaData != null)
                {
                    return metaData;
                }

                if (FileInfo.Extension.Equals(".png", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        var frame = BitmapFrame.Create(new Uri(FileInfo.FullName));

                        if (frame.Metadata == null)
                        {
                            metaData = new ImageDetail() { Width = frame.PixelWidth, Height = frame.PixelHeight, };
                            return metaData;
                        }

                        var m = (BitmapMetadata)frame.Metadata;

                        if (m.Any(query => query.StartsWith("/tEXt")))
                        {
                            metaData = new ImageDetail(m.GetQuery("/tEXt/{str=parameters}") as string)
                                { Width = frame.PixelWidth, Height = frame.PixelHeight, };

                            return metaData;
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                        Debug.WriteLine("画像の読み込みに失敗しました");
                        throw;
                    }
                }

                return metaData;
            }
        }

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