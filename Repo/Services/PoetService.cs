using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using DAL;
using Repo.Iservices;

public class PoetService : IPoetService
{
    private readonly PoetDAL _poetDAL;

    public PoetService(PoetDAL poetDAL)
    {
        _poetDAL = poetDAL;
    }

    public async Task InsertPoetAsync(PoetModel poet)
    {
        await _poetDAL.Insert(poet);
    }

    public async Task<PoetModel> GetPoetByEmailAsync(string email)
    {
        return await _poetDAL.GetByEmail(email);
    }

    public async Task UpdatePoetAsync(PoetModel poet)
    {
        await _poetDAL.Update(poet);
    }

    public async Task UpdateUserRoleToPoetAsync(string uId)
    {
        await _poetDAL.UpdateUserRole(uId);
    }

    public async Task<List<PoetModel>> GetAllPoetsAsync()
    {
        return await _poetDAL.GetAllPoetsAsync();
    }

    public async Task<List<PoetModel>> GetUnapprovedPoetsAsync()
    {
        return await _poetDAL.GetUnapprovedPoetsAsync();
    }

    public async Task ApprovePoetAsync(string email = null)
    {
        await _poetDAL.ApprovePoetAsync(email);
    }
    public async Task<string> GetPoetIdByEmail(string email)
    {
        return await _poetDAL.GetPoetIdByEmail(email);
    }
    public async Task UpdateUserRoleToUserAsync(string uId) 
    {
        await _poetDAL.UpdateUserRoleToUser(uId);
    }
}
