using System.Collections.Generic;
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

            //Refactor
            ViewBag.CreatedUsers = Session["createdUsers"];
            ViewBag.UpdatedUsers = Session["updatedUsers"];
            ViewBag.DeletedUsers = Session["deletedUsers"];

            if (Request.IsAjaxRequest())
            {
                return PartialView("_UsersTable", model);
            }

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
            //if (Session["createdUsers"] == null)
            //{
            //    Session["createdUsers"] = new List<UserViewModel>();
            //}

            //var createdUsers = Session["createdUsers"] as List<UserViewModel>;
            //createdUsers?.Add(user);

            var bllUser = user.ToBllModel();
            _userService.CreateUser(bllUser);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_UserRow", user);
            }

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
            if (Session["updatedUsers"] == null)
            {
                Session["updatedUsers"] = new List<UserViewModel>();
            }

            if (ModelState.IsValid)
            {
                var updatedUsers = Session["updatedUsers"] as List<UserViewModel>;
                updatedUsers?.Add(user);

                //var selectedRewards = user.Rewards
                //    .Where(_ => _.IsSelected == true)
                //    .Select(_ => _.ToBllModel())
                //    .ToList();
                //var bllUser = user.ToBllModel(selectedRewards);

                //_userService.UpdateUser(bllUser);

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
            if (Session["deletedUsers"] == null)
            {
                Session["deletedUsers"] = new List<int>();
            }

            var deletedUsers = Session["deletedUsers"] as List<int>;
            deletedUsers?.Add(id);

            //var user = _userService.GetUserById(id);
            //if (user != null)
            //{
            //    _userService.DeleteUser(user);
            //}

            return RedirectToAction("Index");
        }

        [Route("award-user/{userId:int}_{awardId:int}")]
        public ActionResult AwardUser(int userId, int awardId)
        {
            var user = _userService.GetUserById(userId);

            //TODO: refactor
            var newReward = _rewardService
                .GetRewardById(awardId);
            if (newReward.User?.Id == userId)
            {
                object model = "This user already has this award!";
                return View("AwardError", model);
            }
            if (newReward.User != null)
            {
                object model = "This award is not vacant!";
                return View("AwardError", model);
            }

            user.Rewards = user.Rewards.Concat(new[] { newReward });
            _userService.UpdateUser(user);

            return RedirectToAction("Index", "User");
        }

        public FileResult DownloadUsers()
        {
            var bytes = _userService.UsersToByteArray();

            return File(bytes, "text", "Users.txt");
        }

        //TODO: refactor
        public ActionResult ApplyChanges(bool apply)
        {
            if (!apply)
            {
                Session["createdUsers"] = null;
                Session["updatedUsers"] = null;
                Session["deletedUsers"] = null;

                return RedirectToAction("Index", "User");
            }

            if (Session["createdUsers"] is List<UserViewModel> createdUsers && createdUsers.Any())
            {
                foreach (var user in createdUsers)
                {
                    var bllUser = user.ToBllModel();
                    _userService.CreateUser(bllUser);
                }
            }

            if (Session["updatedUsers"] is List<UserViewModel> updatedUsers && updatedUsers.Any())
            {
                foreach (var user in updatedUsers)
                {
                    var selectedRewards = user.Rewards
                        .Where(_ => _.IsSelected == true)
                        .Select(_ => _.ToBllModel())
                        .ToList();
                    var bllUser = user.ToBllModel(selectedRewards);

                    _userService.UpdateUser(bllUser);
                }
            }

            if (Session["deletedUsers"] is List<int> deletedUsers && deletedUsers.Any())
            {
                foreach (var id in deletedUsers)
                {
                    var user = _userService.GetUserById(id);
                    if (user != null)
                    {
                        _userService.DeleteUser(user);
                    }
                }
            }

            Session["createdUsers"] = null;
            Session["updatedUsers"] = null;
            Session["deletedUsers"] = null;


            return RedirectToAction("Index", "User");
        }
    }
}