using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Entities;
using BLL.Interface;
using MVCPL.Util.Helpers;
using MVCPL.Models;
using MVCPL.Util.Models;

namespace MVCPL.Controllers
{
    public class RewardController : Controller
    {
        private readonly IRewardService rewardService;
        private readonly IUserService userService;

        public RewardController(IRewardService rewardService, IUserService userService)
        {
            this.rewardService = rewardService;
            this.userService = userService;
        }

        public ActionResult Index()
        {
            var model =
                rewardService.GetAllRewards()
                    .Select(r => new RewardViewModel
                    {
                        Id = r.Id,
                        Description = r.Description,
                        Title = r.Title,
                        Image = r.Image == null ? null : (HttpPostedFileBase)new MemoryPostedFile(r.Image),
                        //User = new UserViewModel
                        //{
                        //    //fuuuuuuuuuuuuuuuuuuuuuuuuuuuuu
                        //    Id = userService.GetUserById((int)r.User).Id,
                        //    Name = userService.GetUserById((int)r.User).Name,
                        //    BirthDate = userService.GetUserById((int)r.User).BirthDate
                        //}
                    }).ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RewardViewModel reward)
        {
            var bllReward = new BllReward
            {
                Id = reward.Id,
                Description = reward.Description,
                Title = reward.Title
            };
            if (reward.Image != null)
            {
                bllReward.Image = ImageHelper.MapPicture(reward.Image);
            }

            rewardService.CreateReward(bllReward);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var reward = rewardService.GetRewardById(id);

            return View(new RewardViewModel
            {
                Id = reward.Id,
                Description = reward.Description,
                Title = reward.Title,
                Image = reward.Image == null ? null : (HttpPostedFileBase)new MemoryPostedFile(reward.Image)
            });
        }

        [HttpPost]
        public ActionResult Update(RewardViewModel reward, HttpPostedFileBase image)
        {
            var bllReward = new BllReward { Id = reward.Id, Description = reward.Description, Title = reward.Title };
            if (image != null)
            {
                bllReward.Image = ImageHelper.MapPicture(image);
            }

            rewardService.UpdateReward(bllReward);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var reward = rewardService.GetRewardById(id);

            return View(new RewardViewModel { Id = reward.Id, Description = reward.Description, Title = reward.Title, Image = reward.Image == null ? null : (HttpPostedFileBase)new MemoryPostedFile(reward.Image) });
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var reward = rewardService.GetRewardById(id);
            if (reward != null)
            {
                rewardService.DeleteReward(reward);
            }

            return RedirectToAction("Index");
        }
    }
}