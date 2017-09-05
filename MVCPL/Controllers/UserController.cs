using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Entities;
using BLL.Interface;
using MVCPL.Util.Helpers;
using MVCPL.Models;
using MVCPL.Util.Models;
using MVCPL.Infrastructure.Mapping;

namespace MVCPL.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IRewardService rewardService;

        public UserController(IUserService userService, IRewardService rewardService)
        {
            this.userService = userService;
            this.rewardService = rewardService;
        }

        public ActionResult Index()
        {
            var model = userService
                .GetAllUsers()
                .Select(_ => _.ToViewModel());

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserViewModel user)
        {
            var bllUser = user.ToBllModel();
            userService.CreateUser(bllUser);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var possibleRewards = rewardService
                .GetAllPossibleRewards(id)
                .Select(_ => _.ToViewModel())
                .ToList();
            var model = userService
                .GetUserById(id)
                .ToViewModel(possibleRewards);

            return View(model);
        }

        [HttpPost]
        public ActionResult Update(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var selectedRewards = user.Rewards
                    .Where(_ => _.IsSelected == true)
                    .Select(_ => _.ToBllModel())
                    .ToList();
                var bllUser = user.ToBllModel(selectedRewards);

                userService.UpdateUser(bllUser);

                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = userService
                .GetUserById(id)
                .ToViewModel();

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = userService.GetUserById(id);
            if (user != null)
            {
                userService.DeleteUser(user);
            }

            return RedirectToAction("Index");
        }

        public FileResult DownloadUsers()
        {
            var bytes = userService.UsersToByteArray();

            return File(bytes, "text", "Users.txt");
        }
    }
}