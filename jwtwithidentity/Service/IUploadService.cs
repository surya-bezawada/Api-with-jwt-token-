using jwtwithidentity.Model;

namespace jwtwithidentity.Service
{
    public interface IUploadService
    {
        Task<FileUploadResponse> UploadFiles(List<IFormFile> files);
       
        Task<IEnumerable<FileDownloadView>> DownloadFiles();
        Task<byte[]> DownloadFile(int id);
    }
}
