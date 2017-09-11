using System.Data.Entity;
using Ninject;
using Ninject.Web.Common;
using BLL.Interface;
using BLL.Services;
using DAL;
using DAL.DTO;
using DAL.Interface;
using DAL.Repos;
using ORM.Db;

namespace DI
{
    public static class ResolvingConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }

        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }

        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
                kernel.Bind<DbContext>().To<HeroesContext>().InRequestScope();
            }
            else
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
                kernel.Bind<DbContext>().To<HeroesContext>().InSingletonScope();
            }

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IRewardService>().To<RewardService>();
            kernel.Bind<IRepository<DalReward>>().To<RewardRepo>();
            kernel.Bind<IRepository<DalUser>>().To<UserRepo>();
        }
    }
}
