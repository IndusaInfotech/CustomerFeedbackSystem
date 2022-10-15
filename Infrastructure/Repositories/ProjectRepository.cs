using Application.Interfaces.Repositories;
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
    public class ProjectRepository : IProjectRepository
    {
        private readonly IRepositoryAsync<Project> _repository;
        private readonly IDistributedCache _distributedCache;

        public ProjectRepository(IDistributedCache distributedCache, IRepositoryAsync<Project> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }

        public IQueryable<Project> Projects => _repository.Entities;

        public async Task DeleteAsync(Project Project)
        {
            await _repository.DeleteAsync(Project);

        }

        public async Task<Project> GetByIdAsync(long ProjectId)
        {
            return await _repository.Entities.Where(p => p.Id == ProjectId).FirstOrDefaultAsync();
        }

        public async Task<List<Project>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<long> InsertAsync(Project Project)
        {
            await _repository.AddAsync(Project);
            //  await _distributedCache.RemoveAsync(CacheKeys.ProjectCacheKeys.ListKey);
            return Project.Id;
        }

        public async Task UpdateAsync(Project Project)
        {
            await _repository.UpdateAsync(Project);
            //  await _distributedCache.RemoveAsync(CacheKeys.ProjectCacheKeys.ListKey);
            //  await _distributedCache.RemoveAsync(CacheKeys.ProjectCacheKeys.GetKey(Project.Id));
        }
    }
}
