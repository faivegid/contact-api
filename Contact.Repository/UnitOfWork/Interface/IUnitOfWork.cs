using Contact.Data.Repository.Interface;
using Contact.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Repository.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {       
        IUserRepository Users { get; }
    }
}
