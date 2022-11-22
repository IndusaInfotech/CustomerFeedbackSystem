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
    public class AnswerRepository : IAnswerRepository
    {
        private readonly IRepositoryAsync<Answer> _repository;
        private readonly IDistributedCache _distributedCache;
        public AnswerRepository(IDistributedCache distributedCache, IRepositoryAsync<Answer> repository)
        {
            _distributedCache = distributedCache;
            _repository = repository;
        }
        public IQueryable<Answer> Answers => _repository.Entities;

        public async Task<List<AnswerInfo>> GetByIdAsync(long? questionId)
        {
            var data =  _repository.Entities.Where(x => x.QuestionId == questionId).ToList();
            List<AnswerInfo> answerInfos = new List<AnswerInfo>();
            data.ForEach(x =>
            {
                AnswerInfo answerInfo = new AnswerInfo();
                answerInfo.QuestionId = x.QuestionId;
                answerInfo.OpetionText = x.OpetionText;
                answerInfos.Add(answerInfo);
            });

            return  answerInfos;
        }
        public async Task<List<Answer>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }
        public async Task<long> InsertAnswerAsync(List<Answer> answers)
        {
            try
            {
                await _repository.AddRangeAsync(answers);
                return answers.Count;
;
            }
            catch(Exception e)
            {
                var data = e.Message;
                return 0;
            }
        }
        
        public async Task<long> InsertAsync(Answer answer)
        {
            try
            {
                await _repository.AddAsync(answer);
                return answer.Id;
            }
            catch (Exception ex)
            {
                var data = ex.Message;
                return 0;
            }
        }

        public Task UpdateAsync(Answer answer)
        {
            throw new NotImplementedException();
        }
    }
}
