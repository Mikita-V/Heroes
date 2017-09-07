namespace BLL.Entities
{
    public class BllReward
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public BllUser User { get; set; }
    }
}
