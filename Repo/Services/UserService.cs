using Repo.Iservices;
using Entities;
using DAL;

public class UserService : IUserService
{
    private readonly UserDAL _userDAL;

    public UserService(UserDAL userDAL)
    {
        _userDAL = userDAL;
    }

    public async Task<List<UserModel>> GetProfiles()
    {
        return await _userDAL.GetAllProfilesAsync();
    }


    public Task<UserModel> GetProfileById(string id)
    {
        return _userDAL.GetProfileById(id);
    }

    public Task<UserModel> GetProfileByEmail(string email)
    {
        return _userDAL.GetProfileByEmail(email);
    }

    public Task CreateProfile(UserModel user)
    {
        return _userDAL.CreateProfile(user);
    }

    public void Delete(string id)
    {
        _userDAL.Delete(id);
    }

    public Task Update(UserModel user)
    {
        return _userDAL.Update(user);
    }
    public Task<bool> DeleteUserProfileAsync(string id)
    {
        return _userDAL.DeleteUserProfileAsync(id);
    }
}
