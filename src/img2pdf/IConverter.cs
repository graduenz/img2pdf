using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace img2pdf
{
    /// <summary> File conversion interface
    /// </summary>
    public interface IConverter
    {
        /// <summary> Converts a set of images to a pdf file
        /// </summary>
        /// <param name="sourceFilePaths">Image file paths</param>
        /// <param name="targetFilePath">Pdf file path</param>
        /// <param name="orientation">Pdf orientation</param>
        void ImagesToPdf(string[] sourceFilePaths, string targetFilePath, PdfOrientation orientation);
    }
}
