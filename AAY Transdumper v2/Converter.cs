using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAY_Transdumper_v2
{
    static class Converter
    {
        public static System.Windows.Media.Imaging.BitmapImage BitmapToImageSource(System.Drawing.Bitmap bitmap)
        {
            if (bitmap != null)
            {
                using (System.IO.MemoryStream memory = new System.IO.MemoryStream())
                {
                    bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                    memory.Position = 0;
                    System.Windows.Media.Imaging.BitmapImage bitmapimage = new System.Windows.Media.Imaging.BitmapImage();
                    bitmapimage.BeginInit();
                    bitmapimage.StreamSource = memory;
                    bitmapimage.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                    bitmapimage.EndInit();

                    return bitmapimage;
                }
            }
            else
                throw new Exception("A bitmap was not present to display as Image");
        }
    }
}
