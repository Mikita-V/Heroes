using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;
using DAL.Interface;
using DAL.Repos;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private readonly DbContext context;
        private IRepository<DalUser> userRepository;
        private IRepository<DalReward> rewardRepository;

        public IRepository<DalUser> Users => userRepository ?? (userRepository = new UserRepo(context));

        public IRepository<DalReward> Rewards => rewardRepository ?? (rewardRepository = new RewardRepo(context));

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            context?.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                context.Dispose();
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
