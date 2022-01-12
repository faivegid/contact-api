using Contact.Models.DomainModels;
using Contact.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace Contact.Repository.Interface
{
    public interface IUserRepository
    {
        Task<IdentityResult> DeleteAsync(UserContact entity);
        Task<UserContact> FindByEmail(string email);
        Task<UserContact> FindUserAsync(string userId);
        Task<IList<UserContact>> GetAllAsync(Expression<Func<UserContact, bool>> expression, Func<IList<UserContact>, IOrderedQueryable<UserContact>> orderby);
        Task<IPagedList<UserContact>> GetPageList(PagingDTO pager, Expression<Func<UserContact, bool>> expression = null, Func<IQueryable<UserContact>, IOrderedQueryable<UserContact>> orderby = null);
        UserContact GetUser(Expression<Func<UserContact, bool>> expression);
        Task<IdentityResult> InsertAsync(UserContact entity);
        Task<IdentityResult> UpdateAsync(UserContact entity);
        Task<IdentityResult> UpdatePassword(UserContact appUser, string password, string newPassword);
    }
}
