using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Iservices
{
    public interface IUserService
    {
        public Task<List<UserModel>> GetProfiles();
        Task<UserModel> GetProfileById(string id);
        Task CreateProfile(UserModel user);
        void Delete(string id);
        Task Update(UserModel user);
        Task<UserModel> GetProfileByEmail(string email);
        Task<bool> DeleteUserProfileAsync(string id);
    }
}
