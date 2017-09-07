using System;
using DAL.DTO;

namespace DAL.Interface
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<DalUser> Users { get; }
        IRepository<DalReward> Rewards { get; }
        void Commit();
        //TODO: void Rollback();
    }
}
