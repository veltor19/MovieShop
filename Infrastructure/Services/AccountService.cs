using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services {
    public class AccountService : IAccountService {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public async Task<UserAccountModel> GetUserAccountDetails(int userId) {
            var user = await _userRepository.GetById(userId);
            if (user == null) return null;

            return new UserAccountModel {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                ProfilePictureUrl = user.ProfilePictureUrl,
                IsLocked = user.IsLocked
            };
        }

        public async Task<bool> UpdateProfile(int userId, UserAccountModel model) {
            var user = await _userRepository.GetById(userId);
            if (user == null) return false;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.DateOfBirth = model.DateOfBirth;
            user.ProfilePictureUrl = model.ProfilePictureUrl;

            await _userRepository.Update(user);
            return true;
        }
    }
}
