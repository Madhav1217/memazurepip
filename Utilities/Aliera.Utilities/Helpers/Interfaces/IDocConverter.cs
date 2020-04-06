using DinkToPdf.Contracts;

namespace Aliera.Utilities.Helpers
{
    public interface IDocConverter
    {
        void ConvertDocToPDF(string templateWithDataFilePath, string pdfPath, string htmlPath, IConverter converter);
        byte[] ConvertHtmlToPDF(IConverter converter, string html);
    }
}
