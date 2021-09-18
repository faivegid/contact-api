using Contact.Models.DomainModels;
using System.Threading.Tasks;

namespace Contact.Logic
{
    public interface ITokenGenerator
    {
        Task<string> CreateTokenAsync(UserContact user);
    }
}