using Contact.Models.DomainModels;
using Contact.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace Contact.Repository.Implementaions
{
    public class UserRepository : IUserRepository
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

        public UserContact GetUser(Expression<Func<UserContact, bool>> expression)
        {
            return _userManager.Users.FirstOrDefault(expression);
        }

        public async Task<IList<UserContact>> GetAllAsync(Expression<Func<UserContact, bool>> expression, Func<IList<UserContact>, IOrderedQueryable<UserContact>> orderby)
        {
            IList<UserContact> query = await _userManager.Users.Where(expression).ToListAsync(); ;
            if (orderby != null)
            {
                query = await orderby(query).ToListAsync();
            }
            return query;
        }
        public async Task<IPagedList<UserContact>> GetPageList(PagingDTO pager, Expression<Func<UserContact, bool>> expression = null, Func<IQueryable<UserContact>, IOrderedQueryable<UserContact>> orderby = null)
        {
            IQueryable<UserContact> query = _userManager.Users;

            if(expression != null)
                query = query.Where(expression);
            
            if(orderby != null)
                query = orderby(query);

            return await query.ToPagedListAsync<UserContact>(pager.PageNumber, pager.PageSize);
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
