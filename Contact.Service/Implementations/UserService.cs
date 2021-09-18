using AutoMapper;
using Contact.Data;
using Contact.Models.DomainModels;
using Contact.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.SecurityTokenService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Repository
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserContact> _userManger;
        private readonly IMapper _mapper;

        public UserService(UserManager<UserContact> userManger)
        {
            _userManger = userManger;
        }

        #region CRUD Operations
        public async Task<bool> UpdateAsync(string userId, UpdateUserDTO updateRequest)
        {
            UserContact user = await _userManger.FindByIdAsync(userId);
            user = _mapper.Map<UserContact>(updateRequest);
            var result = await _userManger.UpdateAsync(user);
            return Result(result);
        }
        public async Task<bool> UpdatePassword(string userId, UpdatePasswordDTO UpdatePassword)
        {
            UserContact user = await GetAsync(userId);
            var result = await _userManger.ChangePasswordAsync(user, UpdatePassword.OldPassword, UpdatePassword.NewPassword);
            return Result(result);
        }
        public async Task<bool> DeleteUser(string userId)
        {
            UserContact user = await GetAsync(userId);
            var result = await _userManger.DeleteAsync(user);
            return Result(result);
        }
        #endregion
        public IEnumerable<UserDTO> GetAllCustomer()
        {
            return _userManger.Users.Select(c => _mapper.Map<UserDTO>(c));
        }
        public async Task<UserDTO> GetByIdAsync(string userId)
        {
            UserContact user = await _userManger.FindByIdAsync(userId);
            if (user != null)
            {
                UserDTO userDTO = _mapper.Map<UserDTO>(user);
                return userDTO;
            }
            throw new BadRequestException("Resource not Found");
        }

        #region Helper Methods
        private async Task<Models.DomainModels.UserContact> GetAsync(string userId)
        {
            Models.DomainModels.UserContact user = await _userManger.FindByIdAsync(userId);
            if (user != null)
            {
                return user;
            }
            throw new BadRequestException("Resource not Found");
        }
        private static bool Result(IdentityResult result)
        {
            return result.Succeeded ? 
                true : 
                throw new BadRequestException(string.Join("\n", result.Errors.Select(x => x.Description)));
        }
        #endregion 
    }
}
