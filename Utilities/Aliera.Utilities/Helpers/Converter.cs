using Aliera.Utilities.Helpers;
using DinkToPdf;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using DinkToPdf.Contracts;
using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;
using Aliera.Utilities.Enumerations;
using Aliera.Utilities.Constants;

namespace Aliera.Utilities.Helpers
{
    public class Converter : IDocConverter
    {
        public void ConvertDocToPDF(string templateWithDataFilePath, string pdfPath, string htmlPath, IConverter converter)
        {
            ConvertDocToHtml(templateWithDataFilePath, pdfPath, htmlPath, converter);
        }
        public void ConvertDocToHtml(string templateWithDataFilePath, string pdfPath, string htmlPath, IConverter converter)
        {
            var fileInfo = new FileInfo(templateWithDataFilePath);
            string fullFilePath = fileInfo.FullName;
            string htmlText = string.Empty;
            htmlText = ParseDOCX(fileInfo);
            var path = $"{htmlPath}{Path.GetFileNameWithoutExtension(templateWithDataFilePath)}.html";
            var writer = File.CreateText(path);
            writer.WriteLine(htmlText.ToString());
            writer.Dispose();
            ConvertHtmlToPDF(converter, path, pdfPath);
        }
        private void ConvertHtmlToPDF(IConverter converter, string htmlFilePath, string pdfPath)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = DinkToPdf.ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = DinkToPdf.PaperKind.A4Plus,
                    Margins = new MarginSettings() { Top = 10 },
                    Out = $"{pdfPath}{Path.GetFileNameWithoutExtension(htmlFilePath)}.pdf"
                                     },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = File.ReadAllText(htmlFilePath),
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                                         }
                              }
            };
            byte[] pdf = converter.Convert(doc);
        }

        public byte[] ConvertHtmlToPDF(IConverter converter, string html)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = DinkToPdf.ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = DinkToPdf.PaperKind.A4Plus,
                    Margins = new MarginSettings() { Top = 10 }
                                     },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8" }
                                         }
                              }
            };
            byte[] pdf = converter.Convert(doc);
            return pdf;
        }

        static Uri FixUri(string brokenUri)
        {
            string newURI = string.Empty;
            if (brokenUri.Contains("mailto:"))
            {
                int mailToCount = "mailto:".Length;
                brokenUri = brokenUri.Remove(0, mailToCount);
                newURI = brokenUri;
            }
            return new Uri(newURI);
        }

        static string ParseDOCX(FileInfo fileInfo)
        {

            byte[] byteArray = File.ReadAllBytes(fileInfo.FullName);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(byteArray, 0, byteArray.Length);
                using (WordprocessingDocument wDoc =
                                            WordprocessingDocument.Open(memoryStream, true))
                {
                    int imageCounter = 0;
                    var pageTitle = fileInfo.FullName;
                    var part = wDoc.CoreFilePropertiesPart;
                    if (part != null)
                        pageTitle = (string)part.GetXDocument()
                                                .Descendants(DC.title)
                                                .FirstOrDefault() ?? fileInfo.FullName;

                    WmlToHtmlConverterSettings settings = new WmlToHtmlConverterSettings()
                    {
                        AdditionalCss = "body { margin: 1cm auto; max-width: 20cm; padding: 0; } td {border: 1px solid #002C77 !important;} table{width:100%;page-break-inside:avoid}",
                        PageTitle = pageTitle,
                        FabricateCssClasses = true,
                        CssClassPrefix = "pt-",
                        RestrictToSupportedLanguages = false,
                        RestrictToSupportedNumberingFormats = false,
                        ImageHandler = imageInfo =>
                        {
                            ++imageCounter;
                            string extension = imageInfo.ContentType.Split('/')[1].ToLower();
                            ImageFormat imageFormat = null;
                            switch (extension)
                            {
                                case MemberConstants.Png:
                                    imageFormat = ImageFormat.Png;
                                    break;
                                case MemberConstants.Gif:
                                case MemberConstants.Tiff:
                                    imageFormat = ImageFormat.Gif;
                                    break;
                                case MemberConstants.Bmp:
                                    imageFormat = ImageFormat.Bmp;
                                    break;
                                case MemberConstants.Jpeg:
                                    imageFormat = ImageFormat.Jpeg;
                                    break;
                                case MemberConstants.Xwmf:
                                    extension = "wmf";
                                    imageFormat = ImageFormat.Wmf;
                                    break;
                                default:
                                    break;
                            }

                            if (imageFormat == null) return null;

                            string base64 = null;

                            using (MemoryStream ms = new MemoryStream())
                            {
                                imageInfo.Bitmap.Save(ms, imageFormat);
                                var ba = ms.ToArray();
                                base64 = System.Convert.ToBase64String(ba);
                            }

                            ImageFormat format = imageInfo.Bitmap.RawFormat;
                            ImageCodecInfo codec = ImageCodecInfo.GetImageDecoders()
                                                        .First(c => c.FormatID == format.Guid);
                            string mimeType = codec.MimeType;

                            string imageSource =
                                    string.Format("data:{0};base64,{1}", mimeType, base64);

                            XElement img = new XElement(Xhtml.img,
                                    new XAttribute(NoNamespace.src, imageSource),
                                    imageInfo.ImgStyleAttribute,
                                    imageInfo.AltText != null ?
                                        new XAttribute(NoNamespace.alt, imageInfo.AltText) : null);
                            return img;
                        }
                    };

                    XElement htmlElement = WmlToHtmlConverter.ConvertToHtml(wDoc, settings);
                    var html = new XDocument(new XDocumentType("html", null, null, null),
                                                                                htmlElement);
                    var htmlString = html.ToString(SaveOptions.DisableFormatting);
                    return htmlString;
                }
            }
        }
    }

}
