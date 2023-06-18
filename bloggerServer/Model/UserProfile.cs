using System.ComponentModel.DataAnnotations;

namespace bloggerServer.Model
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Description { get;set; }
        public string? PinnedOne { get; set; }
        public string? PinnedTwo { get; set; }
        public string? PinnedThree { get; set; }

    }
}
