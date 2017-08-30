using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Entities;
using BLL.Interface;
using MVCPL.Infrastructure.Helpers;
using MVCPL.Models;

namespace MVCPL.Controllers
{
    public class RewardController : Controller
    {
        private readonly IRewardService rewardService;

        public RewardController(IRewardService rewardService)
        {
            this.rewardService = rewardService;
        }

        public ActionResult Index()
        {
            var model =
                rewardService.GetAllRewards()
                    .Select(r => new RewardViewModel { Id = r.Id, Description = r.Description, Title = r.Title, Photo = r.Image })
                    .ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RewardViewModel reward, HttpPostedFileBase image)
        {
            var bllReward = new BllReward { Id = reward.Id, Description = reward.Description, Title = reward.Title };
            if (image != null)
            {
                bllReward.Image = ImageHelper.MapPicture(image);
            }

            rewardService.CreateReward(bllReward);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var reward = rewardService.GetRewardById(id);

            return View(new RewardViewModel { Id = reward.Id, Description = reward.Description, Title = reward.Title, Photo = reward.Image });
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

            return View(new RewardViewModel { Id = reward.Id, Description = reward.Description, Title = reward.Title, Photo = reward.Image });
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