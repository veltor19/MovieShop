using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories {
    public interface IRepository<T> where T: class {
        //CRUD
        Task<T> Insert(T entity);
        Task<T> DeleteById(int id);
        Task<T> Update(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);

    }
}
