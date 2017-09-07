using System;
using System.Data.Entity;
using DAL.DTO;
using DAL.Interface;
using DAL.Repos;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;
        private readonly DbContext _context;
        private IRepository<DalUser> _userRepository;
        private IRepository<DalReward> _rewardRepository;

        public IRepository<DalUser> Users => _userRepository ?? (_userRepository = new UserRepo(_context));

        public IRepository<DalReward> Rewards => _rewardRepository ?? (_rewardRepository = new RewardRepo(_context));

        public UnitOfWork(DbContext context)
        {
            this._context = context;
        }

        public void Commit()
        {
            _context?.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                return;
            }

            if (disposing)
            {
                _context.Dispose();
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
