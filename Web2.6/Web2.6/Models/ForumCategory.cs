using System.ComponentModel.DataAnnotations;

namespace Web2._6.Models
{
    public class ForumCategory
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}
