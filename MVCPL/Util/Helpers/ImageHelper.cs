using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace MVCPL.Util.Helpers
{
    public static class ImageHelper
    {
        public static byte[] MapPicture(HttpPostedFileBase image)
        {
            if (image == null)
            {
                return null;
            }

            using (var ms = new MemoryStream())
            {
                image.InputStream.CopyTo(ms);
                var bytes = ms.GetBuffer();

                return bytes;
            }
        }
    }
}