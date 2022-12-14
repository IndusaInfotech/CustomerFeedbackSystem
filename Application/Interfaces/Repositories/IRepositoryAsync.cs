
using Application.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IRepositoryAsync<T> where T : class
    {
        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(long id);

        Task<List<T>> GetAllAsync();

        Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize);

        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IList<T> objs, bool saveChanges = true);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
        Task UpdateAsync(SurveyInfo surveys);
    }
}
