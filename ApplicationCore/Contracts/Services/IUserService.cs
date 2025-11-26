using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services {
    public interface IUserService {
        public Task<bool> RegisterUser(RegisterRequestModel registerRequestModel);

        public Task<User> ValidateUser(string email, string password);

        public Task<User> GetUserByEmail(string email);


    }
}
