using Contact.Models.DomainModels;
using Contact.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace Contact.Repository.Implementaions
{
    public interface IUserRepository
    {
        Task<bool> DeleteAsync(UserContact entity);
        UserContact GetUser(Expression<Func<UserContact, bool>> expression);
        Task<IList<UserContact>> GetAllAsync(
            Expression<Func<UserContact, bool>> expression = null,
            Func<IList<UserContact>, IOrderedQueryable<UserContact>> orderby = null);
        Task<IPagedList<UserContact>> GetPageList(
            PagingDTO pager,
            Expression<Func<UserContact, bool>> expression = null,
            Func<IQueryable<UserContact>, IOrderedQueryable<UserContact>> orderby = null);
        Task<bool> InsertAsync(UserContact entity);
        Task<bool> UpdateAsync(UserContact entity);
        Task<bool> UpdatePassword(UserContact appUser, string password, string newPassword);
    }
}