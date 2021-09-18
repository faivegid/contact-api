using Contact.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contact.Repository.Implementaions
{
    public interface IUserRepository
    {
        Task<bool> DeleteAsync(UserContact entity);
        UserContact Get(Expression<Func<UserContact, bool>> expression, List<string> Includes = null);
        IList<UserContact> GetAllAsync(Expression<Func<UserContact, bool>> expression = null, Func<IQueryable<UserContact>, IOrderedQueryable<UserContact>> orderby = null, List<string> Includes = null);
        Task<bool> InsertAsync(UserContact entity);
        Task<bool> UpdateAsync(UserContact entity);
        Task<bool> UpdatePassword(UserContact appUser, string password, string newPassword);
    }
}