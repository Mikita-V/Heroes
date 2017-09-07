using System.IO;
using System.Web;

namespace MVCPL.Util.HelperModels
{
    public class MemoryPostedFile : HttpPostedFileBase
    {
        private readonly byte[] fileBytes;

        public MemoryPostedFile(byte[] fileBytes, string fileName = null)
        {
            this.fileBytes = fileBytes;
            this.FileName = fileName;
            this.InputStream = new MemoryStream(fileBytes);
        }

        public static MemoryPostedFile CreateInstance(byte[] fileBytes)
        {
            if (fileBytes == null)
            {
                return null;
            }

            return new MemoryPostedFile(fileBytes);
        }

        public override int ContentLength => fileBytes.Length;

        public override string FileName { get; }

        public override Stream InputStream { get; }
    }
}