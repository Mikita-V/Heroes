using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace MVCPL.Infrastructure.Helpers
{
    public static class ImageHelper
    {
        public static byte[] MapPicture(HttpPostedFileBase image)
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