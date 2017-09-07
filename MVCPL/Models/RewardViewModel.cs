using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MVCPL.Models
{
    public class RewardViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z\d-]*$")]
        public string Title { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        //[Required]
        public HttpPostedFileBase Image { get; set; }

        public UserViewModel User { get; set; }

        public bool IsSelected { get; set; }
    }
}