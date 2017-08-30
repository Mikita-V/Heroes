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
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Index()
        {
            var model =
                userService.GetAllUsers()
                    .Select(u => new UserViewModel { Id = u.Id, Name = u.Name, BirthDate = u.BirthDate, Photo = u.Photo })
                    .ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserViewModel user, HttpPostedFileBase image)
        {
            var bllUser = new BllUser { Id = user.Id, Name = user.Name, BirthDate = user.BirthDate };
            if (image != null)
            {
                bllUser.Photo = ImageHelper.MapPicture(image);
            }

            userService.CreateUser(bllUser);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var user = userService.GetUserById(id);

            return View(new UserViewModel { Id = user.Id, Name = user.Name, BirthDate = user.BirthDate, Photo = user.Photo });
        }

        [HttpPost]
        public ActionResult Update(UserViewModel user, HttpPostedFileBase image)
        {
            var bllUser = new BllUser { Id = user.Id, Name = user.Name, BirthDate = user.BirthDate };
            if (image != null)
            {
                bllUser.Photo = ImageHelper.MapPicture(image);
            }

            userService.UpdateUser(bllUser);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var user = userService.GetUserById(id);

            return View(new UserViewModel { Id = user.Id, Name = user.Name, BirthDate = user.BirthDate });
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