using Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly IRepositoryAsync<ResultInfo> _repository;
        private readonly IDistributedCache _distributedCache;

        public ResultRepository(IDistributedCache distributedCache, IRepositoryAsync<ResultInfo> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<ResultInfo> Results => _repository.Entities;

        IQueryable<AspNetCoreHero.Results.Result> IResultRepository.Results => throw new NotImplementedException();

        public async Task DeleteAsync(ResultInfo result)
        {
            await _repository.DeleteAsync(result);
        }


       
        public async Task<ResultInfo> GetByIdAsync(long resultId)
        {
            return await _repository.Entities.Where(r => r.Id == resultId).FirstOrDefaultAsync();
        }
        public async Task<List<ResultInfo>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }
        public async Task<long> InsertAsync(ResultInfo result)
        {
            await _repository.AddAsync(result);
            
            return result.Id;
        }

        
        

        public async Task UpdateAsync(ResultInfo result)
        {
            await _repository.UpdateAsync(result);
       
        }

        Task<Result> IResultRepository.GetByIdAsync(long resultId)
        {
            throw new NotImplementedException();
        }

        Task<List<Result>> IResultRepository.GetListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
