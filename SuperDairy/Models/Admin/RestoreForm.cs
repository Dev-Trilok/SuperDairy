using System.ComponentModel.DataAnnotations;

namespace SuperDairy.Models.Admin
{
    public class RestoreForm
    {
        [Required]
        public IFormFile BackupFile { get; set; }
    }
}
