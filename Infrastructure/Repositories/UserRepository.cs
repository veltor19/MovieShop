using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories {
    public class UserRepository : BaseRepository<User>, IUserRepository {

        public UserRepository(MovieShopDbContext movieShopDbContext) : base(movieShopDbContext) {

        }
        public async Task<User> GetByEmail(string email) {
            var user = await _movieShopDbContext.Users.FirstOrDefaultAsync(a => a.Email == email);
            return user;
        }
    }
}
