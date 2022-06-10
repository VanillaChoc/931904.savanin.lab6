using System.ComponentModel.DataAnnotations;

namespace Web2._6.Models
{
    public class Folder
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int ParentFolderId { get; set; }
        public List<Folder> Folders { get; set; } = new List<Folder>();
        public List<UserFile> Files { get; set; } = new List<UserFile>();
    }
}
