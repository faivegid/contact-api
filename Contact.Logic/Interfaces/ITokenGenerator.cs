using Contact.Models;
using Contact.Models.DomainModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Contact.Logic
{
    public interface ITokenGenerator
    {
        Task<string> GenerateTokenAsync(Models.DomainModels.UserContact user);
    }
}