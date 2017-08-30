namespace ORM.Entities
{
    public class Reward
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public virtual User User { get; set; }
    }
}