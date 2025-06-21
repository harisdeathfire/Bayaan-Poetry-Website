using DAL;
using Entities;
using Repo.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repo.Services
{
    public class GhazalService : IGhazalService
    {
        private readonly GhazalDAL _dal;

        public GhazalService(GhazalDAL dal)
        {
            _dal = dal;
        }

        public async Task<List<GhazalModel>> GetAllGhazals()
        {
            return await _dal.GetAllGhazals();
        }

        public async Task<GhazalModel> GetGhazalById(string ghazalId)
        {
            return await _dal.GetGhazalById(ghazalId);
        }

        public async Task InsertGhazal(GhazalModel ghazal)
        {
            await _dal.InsertGhazal(ghazal);
        }

        public async Task<bool> DeleteGhazal(string ghazalId)
        {
            return await _dal.DeleteGhazal(ghazalId);
        }

        public async Task<List<GhazalModel>> GetGhazalsByPoetIdOrNickname(string poetId, string nickname)
        {
            return await _dal.GetGhazalsByPoetIdOrNickname(poetId, nickname);
        }

        public async Task<List<GhazalModel>> GetGhazalsByEmailAsync(string email)
        {
            return await _dal.GetGhazalsByEmail(email);
        }

        public async Task<List<GhazalModel>> GetGhazalsByNicknameAsync(string nickname) 
        {
            return await _dal.GetGhazalsByNicknameAsync(nickname);
        }
    }
}
