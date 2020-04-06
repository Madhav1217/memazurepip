using System;
using Microsoft.Extensions.Options;
using DinkToPdf;
using System.IO;
using Aliera.Utilities.Constants;
using DinkToPdf.Contracts;
using Renci.SshNet.Common;
using Renci.SshNet;
using Aliera.Utilities.Encoding;

namespace Aliera.Utilities.FileUpload
{
    public class FileUploadService : IFileUploadService
    {
        private static string _fTPPassword;

        public byte[] UploadSignatureAsync(long memberId, string documentPath, string fileName, string html, IConverter converter,
            IOptions<AppSettings> appSettings)
        {
            var dinkToPDFDllPath = Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll");
            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(dinkToPDFDllPath);
            var directoryPath = Path.GetDirectoryName(documentPath);
            if (!File.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var fullFilePath = Path.Combine(directoryPath, fileName + BrokerConstants.PDF_EXTENTION);

            var doc = new HtmlToPdfDocument
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4Plus,
                    Margins = new MarginSettings { Top = 10 },
                    Out = fullFilePath
                },
                Objects = {
                    new ObjectSettings{
                        PagesCount = true,
                        HtmlContent = HtmlEncodeDecode.HtmlDecode(html),
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            byte[] file;
            file = converter.Convert(doc);

            if (file != null)
            {
                var isUploadedToFTP = UploadSignatureDocumentToFTP(appSettings, fullFilePath);
                file = File.ReadAllBytes(fullFilePath);

                if (isUploadedToFTP)
                {
                    File.Delete(fullFilePath);
                    Directory.Delete(directoryPath);
                }
            }
            return file;
        }

        private static bool UploadSignatureDocumentToFTP(IOptions<AppSettings> ftpSettings, string file)
        {
            _fTPPassword = ftpSettings.Value.FTPPassword;
            var fileInfo = new FileInfo(file);
            var kauth = new KeyboardInteractiveAuthenticationMethod(ftpSettings.Value.FTPUsername);
            var pauth = new PasswordAuthenticationMethod(ftpSettings.Value.FTPUsername, _fTPPassword);
            kauth.AuthenticationPrompt += new EventHandler<AuthenticationPromptEventArgs>(HandleKeyEvent);
            var connectionInfo = new ConnectionInfo(ftpSettings.Value.SingnatureDocumentFTPPath, 22, ftpSettings.Value.FTPUsername, pauth, kauth);

            using (var sftp = new SftpClient(connectionInfo))
            {
                sftp.Connect();
                using (var fileStream = new FileStream(file, FileMode.Open))
                {
                    sftp.UploadFile(fileStream, "/" + Path.GetFileName(file));
                }
                sftp.Disconnect();
            }
            return true;
        }

        private static void HandleKeyEvent(object sender, AuthenticationPromptEventArgs e)
        {
            foreach (AuthenticationPrompt prompt in e.Prompts)
            {
                if (prompt.Request.IndexOf("Password:", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    prompt.Response = _fTPPassword;
                }
            }
        }

    }
}
