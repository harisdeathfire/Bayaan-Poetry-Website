using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repo.Iservices
{
    public interface IPoetService
    {
        Task InsertPoetAsync(PoetModel poet);
        Task<PoetModel> GetPoetByEmailAsync(string email);
        Task UpdatePoetAsync(PoetModel poet);
        Task UpdateUserRoleToPoetAsync(string uId);
        Task<List<PoetModel>> GetAllPoetsAsync();
        Task ApprovePoetAsync(string email = null);
        Task<List<PoetModel>> GetUnapprovedPoetsAsync();
        Task<string> GetPoetIdByEmail(string email);
        Task UpdateUserRoleToUserAsync(string uId);


    }
}
