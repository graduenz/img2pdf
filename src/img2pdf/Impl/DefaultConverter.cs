using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace img2pdf.Impl
{
    /// <summary> Default converter implementation
    /// </summary>
    public class DefaultConverter : IConverter
    {
        private const int BORDER_SIZE = 96;

        /// <summary> Converts a set of images to a pdf file
        /// </summary>
        /// <param name="sourceFilePaths">Image file paths</param>
        /// <param name="targetFilePath">Pdf file path</param>
        /// <param name="orientation">Pdf orientation</param>
        public void ImagesToPdf(string[] sourceFilePaths, string targetFilePath, PdfOrientation orientation)
        {
            Rectangle pageSize = orientation == PdfOrientation.A4_Portrait ? PageSize.A4
                : orientation == PdfOrientation.A4_Landscape ? PageSize.A4.Rotate()
                : throw new ArgumentException($"Unknown orientation {orientation}", nameof(orientation));

            var document = new Document(pageSize, BORDER_SIZE, BORDER_SIZE, BORDER_SIZE, BORDER_SIZE);
            using (var stream = new FileStream(targetFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                PdfWriter.GetInstance(document, stream);
                document.Open();

                for (int i = 0; i < sourceFilePaths.Length; ++i)
                {
                    string imagePath = sourceFilePaths[i];

                    using (var imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        var image = Image.GetInstance(imageStream);

                        if (image.Height > pageSize.Height - BORDER_SIZE) {
                            image.ScaleToFit(pageSize.Width - BORDER_SIZE, pageSize.Height - BORDER_SIZE);
                        }
                        else if (image.Width > pageSize.Width - BORDER_SIZE) {
                            image.ScaleToFit(pageSize.Width - BORDER_SIZE, pageSize.Height - BORDER_SIZE);
                        }

                        image.Alignment = Image.ALIGN_MIDDLE;

                        if (i > 0) {
                            document.NewPage();
                        }

                        document.Add(image);
                    }
                }

                document.Close();
            }
        }
    }
}
