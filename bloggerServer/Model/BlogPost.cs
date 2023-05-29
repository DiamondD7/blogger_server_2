using System.ComponentModel.DataAnnotations;

namespace bloggerServer.Model
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }

        public int PostUserId { get; set; }

        [Required]
        public string PostTitle { get; set; }
        public string? PostBody { get; set; }
        public string? Description { get; set; }
        public IFormFile? PostImage { get; set; }
        public string? PostImagePath { get; set; }
        public DateTime CreatedOn { get; set; }


    }
}
