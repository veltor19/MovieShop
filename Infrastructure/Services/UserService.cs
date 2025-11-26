using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services {
    public class UserService: IUserService {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByEmail(string email) {
            return await _userRepository.GetByEmail(email);
        }

        public async Task<bool> RegisterUser(RegisterRequestModel model) {
            var existingUser = await _userRepository.GetByEmail(model.Email);
            if (existingUser != null) {
                return false;
            }

            var salt = CreateSalt();
            var hashPassword = HashPassword(model.Password, salt);
            var user = new User {
                FirstName = model.FirstName,
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                HashedPassword = hashPassword,
                Salt = salt,
                IsLocked = false,
                ProfilePictureUrl = "example.com"
            };

            await _userRepository.Insert(user);
            return true;
        }

        public async Task<User> ValidateUser(string email, string password) {
            var user = await _userRepository.GetByEmail(email);
            if (user == null) {
                return null;
            }

            var hashedPassword = HashPassword(password, user.Salt);
            if (hashedPassword == user.HashedPassword) {
                return user;
            }
            return null;

        }

        private string CreateSalt() {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        private string HashPassword(string password, string salt) {
            return password;
        }
    }
}
