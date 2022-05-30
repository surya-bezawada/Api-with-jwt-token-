namespace jwtwithidentity.Model
{
    public class FileUploadResponse
    {
        public string ErrorMessage { get; set; }
        public List<FileUploadResponseData> Data { get; set; }
    }
}
