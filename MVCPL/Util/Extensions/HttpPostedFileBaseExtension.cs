using System;
using System.IO;
using System.Web;

namespace MVCPL.Util.Extensions
{
    public static class HttpPostedFileBaseExtension
    {
        public static string ToBase64String(this HttpPostedFileBase image)
        {
            return image == null ? null : Convert.ToBase64String(image.ToBytes());
        }

        public static byte[] ToBytes(this HttpPostedFileBase image)
        {
            return image == null ? null : ToByteArray(image);
        }

        private static byte[] ToByteArray(HttpPostedFileBase image)
        {
            using (var ms = new MemoryStream())
            {
                image.InputStream.CopyTo(ms);
                var bytes = ms.GetBuffer();

                return bytes;
            }
        }
    }
}