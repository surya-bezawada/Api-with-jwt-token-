using System.ComponentModel.DataAnnotations;

namespace jwtwithidentity.Model
{
    public class UploadFiles
    {

        [Key]
        [Required]
       
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public long FileSize { get; set; }

        [Required]
        public byte[] FileContent { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }


        public string UploadedBy { get; set; }

        public string FilePath { get; set; }

        public string DbPath { get; set; }
    }
}
