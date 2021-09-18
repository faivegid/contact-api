using Contact.DTO;
using System.Threading.Tasks;

namespace Contact.Logic
{
    public interface IAuthenticator
    {
        /// <summary>
        /// Passes a login request and checks if the credential are valid
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>A userDTO that represents the user model</returns>
        Task<UserDTO> Login(LoginRequest loginRequest);

        /// <summary>
        /// registers a user with the incoming data from the DTO RegisterRequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns>UserDTO which is the DTO for the user just created</returns>
        Task<UserDTO> Register(RegisterRequest request);
    }
}