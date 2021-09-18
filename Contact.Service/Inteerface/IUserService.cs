using Contact.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contact.Data
{
    public interface IUserService
    {
        Task<bool> DeleteUser(string userId);
        IEnumerable<UserDTO> GetAllCustomer();

        /// <summary>
        /// Udpate the user basic information 
        /// </summary>
        /// <param name="userId">the id of the user that the information is to be updated</param>
        /// <param name="updateRequest">the data object that has the parameters that needs to be updated</param>
        /// <returns>true if the  operation succeds</returns>
        Task<bool> UpdateAsync(string userId, UpdateUserDTO updateRequest);
        Task<bool> UpdatePassword(string userId, UpdatePasswordDTO UpdatePassword);
        Task<UserDTO> GetByIdAsync(string userId);
    }
}