using Contact.Models.DomainModels;
using Contact.Models.DTOs;
using Contact.Repository.Interface;
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

        public UserContact GetUser(Expression<Func<UserContact, bool>> expression) =>
            _userManager.Users.FirstOrDefault(expression);

        public async Task<UserContact> FindUserAsync(string userId) =>
            await _userManager.FindByIdAsync(userId);

        public async Task<UserContact> FindByEmail(string email) =>
            await _userManager.FindByEmailAsync(email);

        public async Task<IdentityResult> InsertAsync(UserContact entity) =>
            await _userManager.CreateAsync(entity);

        public async Task<IdentityResult> UpdateAsync(UserContact entity) =>
            await _userManager.UpdateAsync(entity);

        public async Task<IdentityResult> UpdatePassword(UserContact appUser, string password, string newPassword) =>
            await _userManager.ChangePasswordAsync(appUser, password, newPassword);

        public async Task<IdentityResult> DeleteAsync(UserContact entity) =>
            await _userManager.DeleteAsync(entity);

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

            if (expression != null)
                query = query.Where(expression);

            if (orderby != null)
                query = orderby(query);

            return await query.ToPagedListAsync(pager.PageNumber, pager.PageSize);
        }
    }
}
