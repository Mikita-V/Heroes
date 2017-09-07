using System.Linq;
using System.Web.Mvc;
using BLL.Interface;
using MVCPL.Models;
using MVCPL.Infrastructure.Mapping;

namespace MVCPL.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRewardService _rewardService;

        public UserController(IUserService userService, IRewardService rewardService)
        {
            this._userService = userService;
            this._rewardService = rewardService;
        }

        //TODO: Null reference
        [Route("users")]
        public ActionResult Index()
        {
            var model = _userService
                .GetAllUsers()
                .Select(_ => _.ToViewModel());

            return View(model);
        }

        [Route("create-user")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //TODO: Null reference, validation
        [Route("create-user")]
        [HttpPost]
        public ActionResult Create(UserViewModel user)
        {
            var bllUser = user.ToBllModel();
            _userService.CreateUser(bllUser);

            return RedirectToAction("Index");
        }

        //TODO: Null reference
        [Route("user/{id:int}/edit")]
        [HttpGet]
        public ActionResult Update(int id)
        {
            var possibleRewards = _rewardService
                .GetAllPossibleRewards(id)
                .Select(_ => _.ToViewModel())
                .ToList();
            var model = _userService
                .GetUserById(id)
                .ToViewModel(possibleRewards);

            return View(model);
        }

        //TODO: Null reference
        [Route("user/{id:int}/edit")]
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

                _userService.UpdateUser(bllUser);

                return RedirectToAction("Index");
            }

            return View(user);
        }

        [Route("user/{id:int}")]
        [HttpGet]
        public ActionResult Details(int id)
        {
            var possibleRewards = _rewardService
                .GetAllPossibleRewards(id)
                .Select(_ => _.ToViewModel())
                .ToList();
            var model = _userService
                .GetUserById(id)
                .ToViewModel(possibleRewards);

            return View(model);
        }

        //TODO: Null reference
        [Route("user/{id:int}/delete")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _userService
                .GetUserById(id)
                .ToViewModel();

            return View(model);
        }

        [Route("user/{id:int}/delete")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = _userService.GetUserById(id);
            if (user != null)
            {
                _userService.DeleteUser(user);
            }

            return RedirectToAction("Index");
        }

        public FileResult DownloadUsers()
        {
            var bytes = _userService.UsersToByteArray();

            return File(bytes, "text", "Users.txt");
        }
    }
}