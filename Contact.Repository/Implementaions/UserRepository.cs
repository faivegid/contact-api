using Contact.Data.Repository.Interface;
using Contact.Models.DomainModels;
using Contact.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Repository.Implementaions
{
    public class UserRepository : IUserRepository, IUserRepository
    {
        private readonly UserManager<UserContact> _userManager;

        public UserRepository(UserManager<UserContact> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> DeleteAsync(UserContact entity)
        {
            var result = await _userManager.DeleteAsync(entity);
            return result.Succeeded;
        }

        public UserContact Get(Expression<Func<UserContact, bool>> expression, List<string> Includes = null)
        {
            return _userManager.Users.FirstOrDefault(expression);
        }

        public IList<UserContact> GetAllAsync(Expression<Func<UserContact, bool>> expression = null, Func<IQueryable<Models.DomainModels.UserContact>, IOrderedQueryable<Models.DomainModels.UserContact>> orderby = null, List<string> Includes = null)
        {
            IQueryable<UserContact> query = _userManager.Users;
            if (expression != null)
                query = query.Where(expression);

            if (orderby != null)
            {
                query = orderby(query);
            }
            return query.ToList();
        }

        public async Task<bool> InsertAsync(UserContact entity)
        {
            var result = await _userManager.CreateAsync(entity);
            return result.Succeeded;
        }

        public async Task<bool> UpdateAsync(UserContact entity)
        {
            var result = await _userManager.UpdateAsync(entity);
            return result.Succeeded;
        }

        public async Task<bool> UpdatePassword(UserContact appUser, string password, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(appUser, password, newPassword);
            return result.Succeeded;
        }
    }
}
