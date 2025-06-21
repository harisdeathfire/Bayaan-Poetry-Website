using DAL;
using Entities;
using Repo.Iservices;
using System.Threading.Tasks;

public class GoogleUserLoginService : IGoogleUserLoginService
{
    private readonly GoogleUserLoginDAL _userLoginDAL;

    public GoogleUserLoginService(GoogleUserLoginDAL userLoginDAL)
    {
        _userLoginDAL = userLoginDAL;
    }

    public async Task SaveUserLogin(GoogleUserLoginModel user)
    {
        await _userLoginDAL.SaveUserLogin(user);
    }

    public async Task<GoogleUserLoginModel?> GetUserLoginByEmail(string email)
    {
        return await _userLoginDAL.GetUserLoginByEmail(email);
    }
}



