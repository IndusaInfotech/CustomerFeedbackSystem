using Application.Interfaces.Repositories;
using Application.Request;
using Domain.Entities;
using EllipticCurve;
using Infrastructure.Migrations.ApplicationDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Question = Domain.Entities.Question;

namespace Infrastructure.Repositories
{
    public class QuestionRepository : IQuestionRepository

    {
        private readonly IRepositoryAsync<Question> _repository;
        private readonly IDistributedCache _distributedCache;
        public QuestionRepository(IDistributedCache distributedCache, IRepositoryAsync<Question> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }


        public IQueryable<Question> Questions => _repository.Entities;



        public async Task DeleteAsync(Question question)
        {
            await _repository.DeleteAsync(question);
        }

      
        public async Task<QuestionInfo> GetByIdAsync(long? questionId  )
        {
            var data = await _repository.Entities.Where(x => x.Id == questionId).FirstOrDefaultAsync();
           
            QuestionInfo question = new QuestionInfo();
           
             question.Id = data.Id;
             question.SurveyId = data.SurveyId;
        
            question.Description = data.Description;
          
            question.NumberOfPage = data.NumberOfPage;
            question.AnswerType = data.AnswerType;

            return question;
        }

        public async Task<List<Question>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

      

        public async Task<long> InsertAsync(Question question)
        {
            try
            {
                await _repository.AddAsync(question);
                //  await _distributedCache.RemoveAsync(CacheKeys.ProjectCacheKeys.ListKey);
                return question.Id;
            }
            catch (Exception ex)
            {
                var data = ex.Message;
                return 0;
            }
        }

        public async Task UpdateAsync(Question question)
        {
            await _repository.UpdateAsync(question);
        }
    }

      

      
    
}
