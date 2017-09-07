using System.Linq;
using System.Web.Mvc;
using BLL.Interface;
using MVCPL.Infrastructure.Mapping;
using MVCPL.Models;

namespace MVCPL.Controllers
{
    public class RewardController : Controller
    {
        private readonly IRewardService _rewardService;

        public RewardController(IRewardService rewardService)
        {
            this._rewardService = rewardService;
        }

        //TODO: Null reference
        public ActionResult Index()
        {
            var model = _rewardService
                .GetAllRewards()
                .Select(_ => _.ToViewModel());

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //TODO: Null reference, validation
        [HttpPost]
        public ActionResult Create(RewardViewModel reward)
        {
            var bllReward = reward.ToBllModel();
            _rewardService.CreateReward(bllReward);

            return RedirectToAction("Index");
        }

        //TODO: Null reference
        [HttpGet]
        public ActionResult Update(int id)
        {
            var reward = _rewardService
                .GetRewardById(id)
                .ToViewModel();

            return View(reward);
        }

        //TODO: Null reference
        [HttpPost]
        public ActionResult Update(RewardViewModel reward)
        {
            if (ModelState.IsValid)
            {
                var bllReward = reward.ToBllModel();
                _rewardService.UpdateReward(bllReward);

                return RedirectToAction("Index");
            }

            return View(reward);
        }

        //TODO: Null reference
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var reward = _rewardService
                .GetRewardById(id)
                .ToViewModel();

            return View(reward);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var reward = _rewardService.GetRewardById(id);
            if (reward != null)
            {
                _rewardService.DeleteReward(reward);
            }

            return RedirectToAction("Index");
        }
    }
}