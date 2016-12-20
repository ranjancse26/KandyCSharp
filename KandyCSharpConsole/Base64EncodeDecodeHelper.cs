using System;
using System.Drawing;
using System.IO;

namespace KandyCSharpConsole
{
    /// <summary>
    /// Reused code from 
    /// http://www.dailycoding.com/posts/convert_image_to_base64_string_and_base64_string_to_image.aspx
    /// </summary>
    public class Base64EncodeDecodeHelper
    {
        public static string ImageToBase64(Image image,
            System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public static Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
    }
}
