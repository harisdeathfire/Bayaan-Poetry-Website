using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repo.IServices
{
    public interface IGhazalService
    {
        Task InsertGhazal(GhazalModel ghazal);
        Task<GhazalModel> GetGhazalById(string ghazalId);
        Task<List<GhazalModel>> GetAllGhazals();
        Task<bool> DeleteGhazal(string ghazalId);
        Task<List<GhazalModel>> GetGhazalsByEmailAsync(string email);
        Task<List<GhazalModel>> GetGhazalsByPoetIdOrNickname(string poetId = null, string nickname = null);
        Task<List<GhazalModel>> GetGhazalsByNicknameAsync(string nickname);

    }
}
