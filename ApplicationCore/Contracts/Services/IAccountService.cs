using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services {
    public interface IAccountService {
        UserAccountModel GetUserAccountDetails(int userId);
        bool UpdateProfile(int userId, UserAccountModel model);
    }
}
