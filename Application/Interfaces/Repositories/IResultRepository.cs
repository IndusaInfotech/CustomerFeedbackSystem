using Application.Request;
using AspNetCoreHero.Results;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IResultRepository
    {
        Task<List<ResultInfo>> GetListAsync();


        Task<Result> GetByIdAsync(long resultId);

        Task<long> InsertAsync(ResultInfo result);

        Task UpdateAsync(ResultInfo result);

        Task DeleteAsync(ResultInfo result);
    }
}
