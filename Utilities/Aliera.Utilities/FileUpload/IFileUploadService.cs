using DinkToPdf.Contracts;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Aliera.Utilities.FileUpload
{
    public interface IFileUploadService
    {
        byte[] UploadSignatureAsync(long memberId, string documentPath, string fileName, string html, IConverter converter, IOptions<AppSettings> appSettings);
    }
}
