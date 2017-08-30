using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;

namespace DAL.Interface
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<DalUser> Users { get; }
        IRepository<DalReward> Rewards { get; }
        void Commit();
        //void Rollback();
    }
}
