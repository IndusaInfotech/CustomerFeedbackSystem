using Application.Interfaces.Repositories;
using Application.Request;
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
    public class SurveyRepository : ISurveyRepository
    {
        private readonly IRepositoryAsync<Survey> _repository;
        private readonly IDistributedCache _distributedCache;

        public SurveyRepository(IDistributedCache distributedCache, IRepositoryAsync<Survey> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }
        public IQueryable<Survey> Surveys => _repository.Entities;

        public async Task DeleteAsync(Survey survey)
        {
            await _repository.DeleteAsync(survey);
        }

        public async Task<SurveyInfo> GetByIdAsync(long? surveyId)
        {
            var data = await _repository.Entities.Where(x => x.Id == surveyId).FirstOrDefaultAsync();
            SurveyInfo survey = new SurveyInfo();
            survey.Id = data.Id;
            survey.Name = data.Name;
            survey.Description = data.Description;
            survey.Title = data.Title;
            survey.NumberOfPage = data.NumberOfPage;
            survey.IsActive = data.IsActive;
            survey.Location = data.Location;

            return survey;
        }

        public async Task<List<Survey>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<long> InsertAsync(Survey survey)
        {
            try
            {
                await _repository.AddAsync(survey);
                //  await _distributedCache.RemoveAsync(CacheKeys.ProjectCacheKeys.ListKey);
                return survey.Id;
            }
            catch(Exception ex)
            {
                var data = ex.Message;
                return 0;
            }
        }

        public async Task UpdateAsync(Survey survey)
        {
            await _repository.UpdateAsync(survey);
            
        }
       
    }
}
