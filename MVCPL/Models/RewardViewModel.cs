namespace MVCPL.Models
{
    public class RewardViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public UserViewModel User { get; set; }
    }
}