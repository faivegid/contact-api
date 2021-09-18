using Contact.Models.DomainModels;
using Contact.Repository.Implementaions;
using Contact.Repository.Interface;
using Contact.Repository.UnitOfWork.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Repository.UnitOfWork.Implementaion
{
    public class UnitOfWork : IUnitOfWork
    {
        private  IUserRepository _userRepository;
        private readonly UserManager<Models.DomainModels.UserContact> _userManager;

        public UnitOfWork(UserManager<Models.DomainModels.UserContact> userManager)
        {
            _userManager = userManager;
        }
        public IUserRepository Users => _userRepository ??= new UserRepository(_userManager);

        public void Dispose()
        {
            _userManager.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
