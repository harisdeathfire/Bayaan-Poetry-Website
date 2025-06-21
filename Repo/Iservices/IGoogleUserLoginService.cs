using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Iservices
{
    public interface IGoogleUserLoginService
    {
        Task SaveUserLogin(GoogleUserLoginModel user);
        Task<GoogleUserLoginModel?> GetUserLoginByEmail(string email);

    }
}
