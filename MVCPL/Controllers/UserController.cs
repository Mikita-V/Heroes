using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface;
using MVCPL.Filters;
using MVCPL.Models;
using MVCPL.Infrastructure.Mapping;

namespace MVCPL.Controllers
{
    //TODO: Separate session and non-session logic
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRewardService _rewardService;

        public UserController(IUserService userService, IRewardService rewardService)
        {
            this._userService = userService;
            this._rewardService = rewardService;
        }

        [Route("users")]
        [Route("users/{searchKey}")]
        public ActionResult Index(string searchKey)
        {
            //TODO: null reference
            var model = searchKey == null
                ? _userService
                    .GetAllUsers()
                    .Select(_ => _.ToViewModel())
                : _userService
                    .GetUserByName(searchKey)
                    .Select(_ => _.ToViewModel());

            //TODO: Refactor
            this.CopySessionInfoToViewBag();

            if (Request.IsAjaxRequest())
            {
                return PartialView("_UsersTable", model);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAjax]
        public ActionResult Create(UserViewModel user)
        {
            //if (ModelState.IsValid)
            //{
            //    var bllUser = user.ToBllModel();
            //    _userService.CreateUser(bllUser);

            //    if (Request.IsAjaxRequest())
            //    {
            //        //TODO: Get user from database or generate ID as GUID
            //        return PartialView("_UserRow", user);
            //    }
            //}

            ////TODO: different behavior if model is not valid
            //return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                if (Session["createdUsers"] == null)
                {
                    Session["createdUsers"] = new List<UserViewModel>();
                }

                var createdUsers = Session["createdUsers"] as List<UserViewModel>;
                createdUsers?.Add(user);

                if (Request.IsAjaxRequest())
                {
                    //TODO: Get user from database or generate ID as GUID
                    this.CopySessionInfoToViewBag();
                    return PartialView("_SessionInfoPartial");
                }
            }

            //TODO: different behavior if model is not valid
            return RedirectToAction("Index");
        }

        //TODO: make ajax-only
        [Route("user/{id:int}/edit")]
        [HttpGet]
        public ActionResult Update(int id)
        {
            //TODO: null reference
            //TODO: include rewards from session
            var possibleRewards = _rewardService
                .GetAllPossibleRewards(id)
                .Select(_ => _.ToViewModel())
                .ToList();
            var model = _userService
                .GetUserById(id)
                .ToViewModel(possibleRewards);

            return PartialView("_EditPartial", model);
        }

        [Route("user/{id:int}/edit")]
        [HttpPost]
        public ActionResult Update(UserViewModel user)
        {
            //if (ModelState.IsValid)
            //{
            //    //TODO: null reference
            //    var selectedRewards = user.Rewards
            //        .Where(_ => _.IsSelected)
            //        .Select(_ => _.ToBllModel())
            //        .ToList();
            //    var bllUser = user.ToBllModel(selectedRewards);
            //    _userService.UpdateUser(bllUser);
            //}

            ////TODO: different behavior if model is not valid
            //return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                if (Session["updatedUsers"] == null)
                {
                    Session["updatedUsers"] = new List<UserViewModel>();
                }

                var updatedUsers = Session["updatedUsers"] as List<UserViewModel>;
                updatedUsers?.Add(user);
            }

            //TODO: different behavior if model is not valid
            return RedirectToAction("Index");
        }

        //TODO: make ajax-only
        [Route("user/{id:int}/delete")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //var user = _userService.GetUserById(id);
            //if (user != null)
            //{
            //    _userService.DeleteUser(user);
            //}

            //var model = _userService
            //    .GetAllUsers()
            //    .Select(_ => _.ToViewModel());

            //return PartialView("_UsersTable", model);

            if (Session["deletedUsers"] == null)
            {
                Session["deletedUsers"] = new List<int>();
            }

            var deletedUsers = Session["deletedUsers"] as List<int>;
            deletedUsers?.Add(id);

            //TODO: update only deleted users
            this.CopySessionInfoToViewBag();

            return PartialView("_SessionInfoPartial");
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

                //TODO: reuse error page
                return View("AwardError", model);
            }
            if (newReward.User != null)
            {
                object model = "This award is not vacant!";

                //TODO: reuse error page
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
                this.ClearSession();

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

            this.ClearSession();


            return RedirectToAction("Index");
        }

        private void CopySessionInfoToViewBag()
        {
            ViewBag.CreatedUsers = Session["createdUsers"];
            ViewBag.UpdatedUsers = Session["updatedUsers"];
            ViewBag.DeletedUsers = Session["deletedUsers"];
        }

        private void ClearSession()
        {
            Session["createdUsers"] = null;
            Session["updatedUsers"] = null;
            Session["deletedUsers"] = null;
        }
    }
}