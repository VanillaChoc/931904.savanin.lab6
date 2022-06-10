using System.ComponentModel.DataAnnotations;

namespace Web2._6.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
