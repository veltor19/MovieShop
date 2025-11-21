using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories {
    public class UserRepository : BaseRepository<User>, IUserRepository {

        public UserRepository(MovieShopDbContext movieShopDbContext) : base(movieShopDbContext) {

        }
        public User GetByEmail(string email) {
            var user = _movieShopDbContext.Users.FirstOrDefault(a => a.Email == email);
            return user;
        }
    }
}
