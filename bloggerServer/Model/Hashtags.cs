namespace bloggerServer.Model
{
    public class Hashtags
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Tags { get; set; }

    }
}
