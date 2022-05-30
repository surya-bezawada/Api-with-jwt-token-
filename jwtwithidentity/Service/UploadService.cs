using jwtwithidentity.Data;
using jwtwithidentity.Model;

namespace jwtwithidentity.Service
{
    public class UploadService : IUploadService
    {
        private readonly ApplicationDbContext _datacontext;

        public UploadService(ApplicationDbContext datacontext)
        {
            _datacontext = datacontext;
        }

        public async Task<FileUploadResponse> UploadFiles(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            List<FileUploadResponseData> uploadedFiles = new List<FileUploadResponseData>();

            try
            {
                foreach (var f in files)
                {
                    string name = f.FileName.Replace(@"\\\\", @"\\");
                    var folderName = Path.Combine("Resources", "Images");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                    if (f.Length > 0)
                    {
                        var memoryStream = new MemoryStream();

                        try
                        {
                            await f.CopyToAsync(memoryStream);

                            // Upload check if less than 2mb!
                            if (memoryStream.Length < 2097152)
                            {
                              
                                var file = new UploadFiles()
                                {
                                    FileName = Path.GetFileName(name),
                                    FileSize = memoryStream.Length,
                                    UploadDate = DateTime.Now,
                                    UploadedBy = "Admin",
                                    FileContent = memoryStream.ToArray(),
                                    FilePath= Path.Combine(pathToSave,folderName),
                                   
                                };
                         
                                _datacontext.UploadFilestb.Add(file);


                                await _datacontext.SaveChangesAsync();

                                uploadedFiles.Add(new FileUploadResponseData()
                                {
                                    Id = file.Id,
                                    Status = "OK",
                                    FileName = Path.GetFileName(name),
                                    ErrorMessage = "",
                                });
                            }
                            else
                            {
                                uploadedFiles.Add(new FileUploadResponseData()
                                {
                                    Id = 0,
                                    Status = "ERROR",
                                    FileName = Path.GetFileName(name),
                                    ErrorMessage = "File " + f + " failed to upload"
                                });
                            }
                        }
                        finally
                        {
                            memoryStream.Close();
                            memoryStream.Dispose();
                        }
                    }
                }
                return new FileUploadResponse() { Data = uploadedFiles, ErrorMessage = "" };
            }
            catch (Exception ex)
            {
                return new FileUploadResponse() { ErrorMessage = ex.Message.ToString() };
            }
        }

        public async Task<IEnumerable<FileDownloadView>> DownloadFiles()
        {
            IEnumerable<FileDownloadView> downloadFiles =
            _datacontext.UploadFilestb.ToList().Select(f =>
                new FileDownloadView
                {
                    Id = f.Id,
                    FileName = f.FileName,
                    FileSize = f.FileContent.Length
                }
            );
            return downloadFiles;
        }


        public async Task<byte[]> DownloadFile(int id)
        {
            try
            {
                var selectedFile = _datacontext.UploadFilestb
                    .Where(f => f.Id == id)
                    .SingleOrDefault();

                if (selectedFile == null)
                    return null;
                return selectedFile.FileContent;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

