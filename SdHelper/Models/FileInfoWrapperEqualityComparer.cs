using System.Collections.Generic;

namespace SdHelper.Models
{
    public class FileInfoWrapperEqualityComparer : IEqualityComparer<FileInfoWrapper>
    {
        public bool Equals(FileInfoWrapper x, FileInfoWrapper y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.FileInfo.FullName == y.FileInfo.FullName;
        }

        public int GetHashCode(FileInfoWrapper obj)
        {
            return obj.FileInfo.FullName.GetHashCode();
        }
    }
}