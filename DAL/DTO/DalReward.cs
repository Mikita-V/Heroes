using DAL.Interface;

namespace DAL.DTO
{
    public class DalReward : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public DalUser User { get; set; }
    }
}
