using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string? PostImagePathName { get; set; }
        public bool isAnon { get; set; }
        public DateTime CreatedOn { get; set; }


    }
}
