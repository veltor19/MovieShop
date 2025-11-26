using ApplicationCore.Contracts.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories {
    public class BaseRepository<T> : IRepository<T> where T : class {
        protected readonly MovieShopDbContext _movieShopDbContext;

        public BaseRepository(MovieShopDbContext movieShopDbContext) {
            _movieShopDbContext = movieShopDbContext;
        }
        public async Task<T> DeleteById(int id) {
            var entity = await _movieShopDbContext.Set<T>().FindAsync(id);
            if (entity != null) {
                _movieShopDbContext.Set<T>().Remove(entity);
                await _movieShopDbContext.SaveChangesAsync();
                return entity;
            }
            return null;
        }

        public async Task<IEnumerable<T>> GetAll() {
            return await _movieShopDbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(int id) {
            return await _movieShopDbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> Insert(T entity) {
            await _movieShopDbContext.Set<T>().AddAsync(entity);
            await _movieShopDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity) {
            _movieShopDbContext.Entry(entity).State = EntityState.Modified;
            await _movieShopDbContext.SaveChangesAsync();
            return entity;
        }
    }

}
