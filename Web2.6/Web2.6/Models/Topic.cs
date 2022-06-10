using System.ComponentModel.DataAnnotations;

namespace Web2._6.Models
{
    public class Topic
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int ParentForumId { get; set; }
    }
}
