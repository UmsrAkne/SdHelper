using System.IO;

namespace SdHelper.Models
{
    public class FileInfoWrapper
    {
        public FileInfoWrapper(FileInfo f)
        {
            FileInfo = f;
        }

        public FileInfo FileInfo { get; private set; }

        public string Name => FileInfo.Name;

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