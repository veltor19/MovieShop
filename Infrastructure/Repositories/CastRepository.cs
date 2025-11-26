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
    public class CastRepository : BaseRepository<Cast>, ICastRepository {
        public CastRepository(MovieShopDbContext context) : base(context) {

        }

        public override async Task<Cast> GetById(int id) {
            return await _movieShopDbContext.Casts.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
