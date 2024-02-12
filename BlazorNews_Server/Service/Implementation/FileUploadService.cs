using BlazorNews_Server.Service.IService;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorNews_Server.Service.Implementation
{
    public class FileUploadService : IFileUploadService
    {
        #region Constructor
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileUploadService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion
        public bool DeleteFile(string fileName)
        {
            try
            {
                var path = $"{_webHostEnvironment.WebRootPath}\\images\\{fileName}";
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(file.Name);
                string fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                var folderDIrectory = $"{_webHostEnvironment.WebRootPath}\\images";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

                var memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);

                if (!Directory.Exists(folderDIrectory))
                    Directory.CreateDirectory(folderDIrectory);

                await using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.WriteTo(fs);
                }

                return fileName;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
